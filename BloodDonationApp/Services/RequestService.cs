namespace BloodDonationApp.Services
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    public class RequestService : IRequestService
    {
        private ApplicationDbContext dbContext;
        private ICenterService centerService;
        private IUserService userService;
        private INotificationService notificationService;

        public RequestService(ApplicationDbContext dbContext, IUserService userService,
                              ICenterService centerService, INotificationService notificationService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.centerService = centerService;
            this.notificationService = notificationService;
        }

        public void AddNewRequestToCenter(RequestInputModel inputModel)
        {
            Center center = this.centerService.GetCenterById(inputModel.CenterId);

            Request request = new Request()
            {
                Description = inputModel.Description,
                Center = center,
                BloodGroup = inputModel.BloodGroup
            };
            
            this.dbContext.Requests.Add(request);
            this.dbContext.SaveChanges();

            var allPotentialDonors = this.userService.GetAllPotentialDonors(request.BloodGroup);
            var sender = this.userService.GetUserById(inputModel.AuthorId);
            foreach (ApplicationUser receiver in allPotentialDonors)
            {
                this.notificationService.AddNotification(request.Description, receiver.Id);
                this.SendEmail(sender, receiver, request.Description);
            }
        }

        public IList<ApplicationUser> Appliers(string requestId)
        {
            return this.dbContext.Users
                .Include(x => x.UserRequests)
                .Where(x => x.UserRequests
                    .Any(y => y.RequestId == requestId)).ToList();
        }
        public void ApplyForRequest(ApplicationUser currentUser, string requestId)
        {
            UserRequest userRequest = new UserRequest()
            {
                UserId = currentUser.Id,
                RequestId = requestId
            };

            this.dbContext.UserRequests.Add(userRequest);
            this.dbContext.SaveChanges();
        }

        public void DeleteRequest(string id)
        {
            var requestToRemove = this.GetRequestById(id);
            this.dbContext.Requests.Remove(requestToRemove);
            this.dbContext.SaveChanges();
        }

        public void EditRequest(string requestId, RequestInputModel inputModel)
        {
            Center center = this.centerService.GetCenterById(inputModel.CenterId);

            Request request = this.dbContext.Requests.FirstOrDefault(x => x.Id == requestId);

            request.Description = inputModel.Description;
            request.Center = center;
            request.BloodGroup = inputModel.BloodGroup;

            this.dbContext.SaveChanges();

            var allPotentialDonors = this.userService.GetAllPotentialDonors(request.BloodGroup);
            var sender = this.userService.GetUserById(inputModel.AuthorId);
            foreach (ApplicationUser receiver in allPotentialDonors)
            {
                this.SendEmail(sender, receiver, request.Description);
            }
        }

        public Request GetRequestById(string id)
        {
            return this.dbContext.Requests.FirstOrDefault(x => x.Id == id);
        }

        public void SendEmail(ApplicationUser sender, ApplicationUser receiver, string mailDescription)
        {
            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(receiver.Email, receiver.FirstName + " " + receiver.LastName));

            // From
            mailMsg.From = new MailAddress(sender.Email, sender.FirstName + " " + sender.LastName);

            // Subject and multipart/alternative Body
            mailMsg.Subject = "Request for Blood Donation";
            string text = mailDescription;
            mailMsg.Body = text;

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            string username = Settings.GetSendGridUsername();
            string password = Settings.GetSendGridPassword();
            NetworkCredential credentials = new NetworkCredential(username, password);
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }

        public void UnApplyForRequest(ApplicationUser currentUser, string requestId)
        {
            UserRequest userRequest = this.dbContext.UserRequests
                .FirstOrDefault(x => x.RequestId == requestId && x.UserId == currentUser.Id);

            this.dbContext.UserRequests.Remove(userRequest);
            this.dbContext.SaveChanges();
        }
    }
}