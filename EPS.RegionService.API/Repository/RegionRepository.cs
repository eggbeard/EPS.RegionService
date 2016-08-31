using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPS.RegionService.Repository.Entities;
using Newtonsoft.Json;

namespace EPS.RegionService.Repository
{
    public class RegionRepository : IRegionRepository
    {
        //HostingEnvironment.MapPath(@"~/App_Data/Region.json"
        private string _filePath;

        public RegionRepository(string datafile)
        {
            filePath = datafile;
        }

        public string filePath
        {
            get { return _filePath; }
            private set { _filePath = value; }
        }

        public Region Create(Region region)
        {
            // Read in the existing Regions
            var Regions = this.ListRegions();

            // Assign a new Id
            int maxId = 0;
            if(Regions.Any())
                maxId = Regions.Max(r => r.Id);
            region.Id = maxId + 1;
            Regions.Add(region);

            WriteData(Regions);
            return region;
        }

        public Region FetchById(int RegionId)
        {
            //Not efficient - Would usually go to datasource with a select where type query
            var regions = ListRegions();
            return regions.FirstOrDefault(r => r.Id == RegionId);
        }

        public IList<Region> ListRegions()
        {
            var json = System.IO.File.ReadAllText(filePath);

            var Regions = JsonConvert.DeserializeObject<List<Region>>(json);

            return Regions;
        }

        public Region Save(int id, Region region)
        {
            // Read in the existing regions
            var regions = this.ListRegions().ToList();

            // Locate and replace the item
            var itemIndex = regions.FindIndex(r => r.Id == id);
            if (itemIndex > -1)
            {
                regions[itemIndex] = region;
            }
            else
            {
                return null;
            }

            WriteData(regions);
            return region;
        }

        private bool WriteData(IList<Region> Regions)
        {
            // Write out the Json
            var json = JsonConvert.SerializeObject(Regions, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }
    }
}
