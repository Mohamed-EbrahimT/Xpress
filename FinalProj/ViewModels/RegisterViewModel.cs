using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProj.ViewModels
{
    public class RegisterViewModel
    {
        
        [StringLength(100)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "please enter correct username")]
        //[DisplayName("EMP NAME")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public int? Age { get; set; }


        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string UserRoleId { get; set; }

       
    }
}
