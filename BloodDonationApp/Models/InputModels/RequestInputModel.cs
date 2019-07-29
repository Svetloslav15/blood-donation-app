using System.ComponentModel.DataAnnotations;

namespace BloodDonationApp.Models.InputModels
{
    public class RequestInputModel
    {
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public string CenterId { get; set; }

        public string AuthorId { get; set; }

        public string BloodGroup { get; set; }
    }
}