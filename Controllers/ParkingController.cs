using System.Collections.Generic;
using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace Invoicing.Controllers
{
    [Route("api/parking")]
    public class ParkingController : Controller
    {
        private readonly IParkingService _service;

        public ParkingController(IParkingService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IEnumerable<Parking> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}", Name = "GetParking")]
        public IActionResult GetById(long id)
        {
            var parking = _service.GetById(id);
            if (parking == null)
            {
                return NotFound();
            }
            return new ObjectResult(parking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Parking parking)
        {
            if (parking == null)
            {
                return BadRequest();
            }

            _service.Create(parking);
            
            return CreatedAtRoute("Getparking", new { id = parking.Id }, parking);
        }
    }
}