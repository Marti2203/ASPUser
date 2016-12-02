using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.CommonFiles.DTO
{
    public class BoxDTOHack
    {    
        //DataTransferObject- Intermediary object between Model and Database Object
        public string ID { get; set; }
        public string Colour { get; set; }
        public string Material { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
