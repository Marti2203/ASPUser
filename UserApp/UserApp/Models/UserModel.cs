using System;
using System.ComponentModel.DataAnnotations;
using UserApp.CommonFiles.Resource;
using System.Web.Mvc;
using static UserApp.Models.SelectListItemFactory;

namespace UserApp.Models
{
    public class UserModel
    {

        public int ID { get; set; }

        [Display(Name = "Username", ResourceType = typeof(UserResources))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Username { get; set; }

        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [MinLength(3, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(ErrorResources))]
        public string SecretAnswer { get; set; }

        [Display(Name = "Agreement", ResourceType = typeof(UserResources))]
        public bool Agreement { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(UserResources))]
        public Gender Gender { get; set; }

        [EmailAddress(ErrorMessageResourceName ="InvalidAddress",ErrorMessageResourceType =typeof(ErrorResources))]
        [Display(Name = "Email", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Email { get; set; }


        [Display(Name = "RetypePassword", ResourceType = typeof(UserResources))]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [MinLength(6, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [RegularExpression("^[A-Z].*", ErrorMessageResourceName = "InvalidRegex", ErrorMessageResourceType = typeof(ErrorResources))]
        public string PasswordSecond { get; set; }

        [Display(Name = "Password", ResourceType = typeof(UserResources))]
        [System.ComponentModel.DataAnnotations.Compare("PasswordSecond")]
        [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [MinLength(6, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [RegularExpression("^[A-Z].*", ErrorMessageResourceName = "InvalidRegex", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Password { get; set; }

        [Display(Name = "SecretQuestion", ResourceType = typeof(UserResources))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string SecretQuestion { get; set; }

        [Display(Name = "About", ResourceType = typeof(UserResources))]
        public string About { get; set; }


        public readonly static SelectListItem[] Questions =
        {
            CrateSecretQuestion("Test"),
            CrateSecretQuestion("Second Test")
        };
    }

}