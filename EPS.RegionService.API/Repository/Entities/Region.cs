namespace EPS.RegionService.Repository.Entities
{
    /// <summary>
    /// A Region defined by zipcodes
    /// </summary>
    public class Region
    {
        public Region()
        {
            ZipCodes = new ZipCodeRanges();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ZipCodeRanges ZipCodes { get; private set; } 
    }
}
