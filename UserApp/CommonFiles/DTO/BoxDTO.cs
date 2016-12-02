using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.CommonFiles.DTO
{
    public class BoxDTO
    {
        //DataTransferObject- Intermediary object between Model and Database Object
        public int ID { get; set; }
        public string Colour { get; set; }
        public string Material { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
    }
}
