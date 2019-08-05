namespace BloodDonationApp.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Center
    {
        public Center()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Requests = new HashSet<Request>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10})|(\+359[0-9]{9})$")]
        [StringLength(10, ErrorMessage = "Phone Number length must be exact 10 symbols!", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}