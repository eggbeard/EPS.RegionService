using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.RegionService.DTO
{
    public class Region
    {
        public Region()
        {
            ZipCodes = new List<string>();
        }
        public int RegionID { get; set; }
        public string Name { get; set; }
        public List<string> ZipCodes { get; set; }
    }
}
