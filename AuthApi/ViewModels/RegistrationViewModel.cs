namespace AuthApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegistrationViewModel
    {
        [MaxLength(15)]
        public string UserName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(15)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(15), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
