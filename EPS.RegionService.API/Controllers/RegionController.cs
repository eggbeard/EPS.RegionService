using EPS.RegionService.Repository;
using EPS.RegionService.Repository.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

//TODO Introduce an IOC controller to do the Repository injection

namespace EPS.RegionService.API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]  // Placeholder for allowing a client, e.g. Angular or Backbone to connect over Cors
    [RoutePrefix("api/region")]
    public class RegionController : ApiController
    {
        IRegionRepository regionRepository;
        RegionFactory _regionFactory = new RegionFactory();

        public RegionController()
        {
            var datafile = HostingEnvironment.MapPath(@"~/App_Data/Region.json");
            regionRepository = new RegionRepository(datafile);
        }

        public IRegionRepository Repository
        {
            get { return regionRepository; }
            private set { regionRepository = value; }
        }

        // GET: api/Region
        // Not async now, but we envisage that our Repository calls may eventually be
        // When backed with a real datasource.
        [ResponseType(typeof(DTO.Region))]
        //public async Task<IHttpActionResult> Get()
        public IHttpActionResult Get()
        {
            try
            {
                var regions = regionRepository.ListRegions();

                return Ok(regions.ToList()
                    .Select(r => _regionFactory.CreateRegion(r)));
            }
            catch(Exception ex) {
                //TODO Log Exceptions.
                Console.WriteLine(ex.Message);
                return InternalServerError();
            }
        }

        // GET: api/Region/5
        [Route("{id}", Name ="GetRegion")]
        [ResponseType(typeof(DTO.Region))]
        public IHttpActionResult Get(int id)
        {
            try {
                var region = Repository.FetchById(id);
                if (region == null)
                {
                    return NotFound();
                }
                else {
                    return Ok(_regionFactory.CreateRegion(region));
                }
            }
            catch {
                return InternalServerError();
            }
        }

        // POST: api/Region
        [ResponseType(typeof(DTO.Region))]
        public IHttpActionResult Post([FromBody]DTO.Region region)
        {
            try {
                if (region == null) {
                    return BadRequest("Cannot parse Region");
                }
                var reg = _regionFactory.CreateRegion(region);
                var result = Repository.Create(reg);

                //if (result == RepositoryActionStatus.Created)
                //{
                    var newRegion = _regionFactory.CreateRegion(result);
                    
                    var href = Url.Link("GetRegion", new { id = newRegion.RegionID });

                    return Created(href, newRegion);
                //}

                //return BadRequest();

            }
            catch (Exception ex) {
                //TODO Log Exceptions
                return InternalServerError();
            }
        }

        // PUT: api/Region/5
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]DTO.Region region)
        {
            try {
                if (region == null)
                {
                    return BadRequest("Cannot parse Region");
                }
                var reg = _regionFactory.CreateRegion(region);

                var upDatedRegion = Repository.Save(id, reg);

                return Ok(upDatedRegion);
            }
            catch {
                return BadRequest();
            }
        }
    }
}
