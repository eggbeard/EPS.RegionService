using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.RegionService.Repository.Entities
{
    /// <summary>
    /// A List of ZipCodeRanges
    /// </summary>
    public class ZipCodeRanges : List<ZipCodeRange>
    {
        public ZipCodeRanges()
        {

        }
        /// <summary>
        /// Parse a list of zipcodes into a list of ranges
        /// </summary>
        /// <param name="zipcodes">The zipcodes to add</param>
        public ZipCodeRanges(List<int> zipcodes)
        {
            this.AddZipCodes(zipcodes);
        }

        public void AddZipCodes(IEnumerable<int> zipcodes)
        {
            //Sort the zipcodes
            var sortedZips = zipcodes.OrderBy(zip => zip);
            try
            {
                var currentRange = new ZipCodeRange();
                foreach (int zipcode in sortedZips)
                {
                    if (!currentRange.TryAdd(zipcode))
                    {
                        this.Add(currentRange);
                        currentRange = new ZipCodeRange(zipcode);
                    }
                }
                this.Add(currentRange);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
