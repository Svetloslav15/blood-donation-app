namespace BloodDonationApp.Services
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;

    public class RequestService : IRequestService
    {
        private ApplicationDbContext dbContext;
        private ICenterService centerService;
        private IUserService userService;

        public RequestService(ApplicationDbContext dbContext, IUserService userService,
                              ICenterService centerService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.centerService = centerService;
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
                this.SendEmail(sender, receiver, request.Description);
            }
        }

        public void ApplyForRequest(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteRequest(string id)
        {
            throw new System.NotImplementedException();
        }

        public void EditRequest(RequestInputModel inputModel)
        {
            throw new System.NotImplementedException();
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
            NetworkCredential credentials = new NetworkCredential("apikey", "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");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }

        public void UnApplyForRequest(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}
