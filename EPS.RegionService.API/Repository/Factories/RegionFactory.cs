using System.Linq;

namespace EPS.RegionService.Repository.Factories
{
    /// <summary>
    /// Convertys between Entity and DTO representations of a Region
    /// </summary>
    public class RegionFactory
    {
        public RegionFactory()
        {

        }

        public DTO.Region CreateRegion(Entities.Region region)
        {
            DTO.Region result = new DTO.Region();
            result.RegionID = region.Id;
            result.Name = region.Name;

            //Expand Ranges to a list
            foreach (Entities.ZipCodeRange ziprange in region.ZipCodes)
            {
                for (int zip = ziprange.Start; zip <= ziprange.End; zip++)
                {
                    result.ZipCodes.Add(zip.ToString("D5"));
                }
            }

            return result;
        }

        //Convert between DTO and Entities.
        //May be netter in a business layer, but at the moment it is not clear what the
        //most useful representation for the Business layer would be.
        public Entities.Region CreateRegion(DTO.Region region)
        {
            Entities.Region result = new Entities.Region();
            result.Id = region.RegionID;
            result.Name = region.Name;
            //Turn list of strings into list of zipcodes 
            var zipcodes = region.ZipCodes.Select(r => int.Parse(r));

            result.ZipCodes.AddZipCodes(zipcodes);

            return result;
        }
    }
}
