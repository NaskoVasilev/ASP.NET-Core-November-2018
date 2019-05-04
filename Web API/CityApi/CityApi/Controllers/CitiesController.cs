using System.Collections.Generic;
using CityApi.Controllers.Common;
using CityApi.Data.Models;
using CityApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CityApi.Controllers
{
    public class CitiesController : ApiController
    {
        private readonly CitiesService service;

        public CitiesController(CitiesService service)
        {
            this.service = service;
        }

        // GET api/cities
        [HttpGet]
        public IEnumerable<CityInfo> Get()
        {
            IEnumerable<CityInfo> cities = service.GetAll();
            return cities;
        }

        // GET api/cities/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CityInfo> Get(int id)
        {
            CityInfo city = service.GetById(id);
            if(city == null)
            {
                return NotFound();
            }

            return city;
        }

        // POST api/cities
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult Post(CityInfo city)
        {
            CityInfo createdCity = this.service.Create(city);

            return CreatedAtAction(nameof(Get), new { id = createdCity.Id }, createdCity);
        }

        // PUT api/cities/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(int id, CityInfo city)
        {
            CityInfo cityInfo = service.GetById(id);
            if(cityInfo == null)
            {
                return NotFound();
            }

            cityInfo.Name = city.Name;
            cityInfo.Population = city.Population;
            service.Update(cityInfo);
            return NoContent();
        }

        // DELETE api/cities/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<CityInfo> Delete(int id)
        {
            CityInfo city = this.service.GetById(id);
            if(city == null)
            {
                return NotFound();
            }

            service.Delete(city);
            return city;
        }
    }
}
