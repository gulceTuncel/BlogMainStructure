using System.ComponentModel.DataAnnotations;

namespace BlogMainStructure.UI.Models.AccountVMs
{
    public class LoginVM
    {
        [Display(Name ="E-mail")]
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
