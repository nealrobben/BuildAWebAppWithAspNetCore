using System.ComponentModel.DataAnnotations;

namespace DutchTreat.Models
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MaxLength(250,ErrorMessage = "Maximum 250 characters")]
        public string Message { get; set; }
    }
}