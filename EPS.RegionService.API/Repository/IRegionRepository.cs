using EPS.RegionService.Repository.Entities;
using System.Collections.Generic;

namespace EPS.RegionService.Repository
{
    //TODO Implement some kind of Repository ActionStatus to allow for better error handling
    //TODO Consider if we need to support a Unit of Work Pattern in the Repository
    public interface IRegionRepository
    {
        Region Create(Region region);
        IList<Region> ListRegions();
        Region FetchById(int RegionId);
        Region Save(int id, Region Region);
    }
}
