using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjektCarRental.Models
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
        public string? ConfirmPassword { get; set; }




    }
}
