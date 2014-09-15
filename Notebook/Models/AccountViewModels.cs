using System.ComponentModel.DataAnnotations;

namespace Notebook.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Username", ResourceType=typeof(Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType=typeof(Resources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType=typeof(Resources))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType=typeof(ErrorMsg), ErrorMessageResourceName="NameRequired")]
        [Display(Name = "Username", ResourceType=typeof(Resources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType=typeof(ErrorMsg), ErrorMessageResourceName="PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType=typeof(Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirmation", ResourceType=typeof(Resources))]
        [Compare("Password", ErrorMessageResourceType=typeof(ErrorMsg), ErrorMessageResourceName="ConfirmationNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}
