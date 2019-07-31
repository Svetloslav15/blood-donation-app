namespace BloodDonationApp.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;
    
    public class PostInputModel
    {
        public string Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }
    }
}