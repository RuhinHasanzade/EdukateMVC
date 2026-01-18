using System.ComponentModel.DataAnnotations;

namespace EdukateProject.ViewModels.AuthViewModels
{
    public class RegisterVm
    {
        [Required]
        [MaxLength(256)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
