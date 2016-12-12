using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace UserApp.CommonFiles.DTO
{
    public class CVDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Experience { get; set; }
        public string Qualities { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }

        public byte[] PictureBytes { get; set; }
        public string PictureName { get; set; }
    }
}
