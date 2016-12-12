using UserApp.CommonFiles.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApp.Models
{
    public class CVModel
    {
        public int ID { get; set; }

        [Display(Name = "FirstName",ResourceType =typeof(CVResources))]
        [Required(AllowEmptyStrings =false,ErrorMessageResourceName ="Required",ErrorMessageResourceType =typeof(ErrorResources))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(CVResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessageResourceName ="InvalidAddress",ErrorMessageResourceType =typeof(ErrorResources))]
        [Display(Name = "Email", ResourceType = typeof(CVResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Email { get; set; }

        [Display(Name = "Experience", ResourceType = typeof(CVResources))]
        public string Experience { get; set; }

        [Display(Name = "Qualities", ResourceType = typeof(CVResources))]
        public string Qualities { get; set; }

        [Display(Name = "Education", ResourceType = typeof(CVResources))]
        public string Education { get; set; }

        [Display(Name = "Address", ResourceType = typeof(CVResources))]
        public string Address { get; set; }

        [Display(Name ="PictureName",ResourceType =typeof(CVResources))]
        public string PictureName { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public byte[] PictureBytes { get; set; }

    }
}