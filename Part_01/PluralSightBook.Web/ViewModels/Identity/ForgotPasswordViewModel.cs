using System.ComponentModel.DataAnnotations;

namespace PluralSightBook.Web.ViewModels.Identity
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}