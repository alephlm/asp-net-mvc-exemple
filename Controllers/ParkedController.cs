using System;
using System.Collections.Generic;
using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace Invoicing.Controllers
{
    [Route("api/parked")]
    public class ParkedController : Controller
    {
        private readonly IParkedService _service;

        public ParkedController(IParkedService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IEnumerable<Parked> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}", Name = "GetParked")]
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
        public IActionResult Create([FromBody] ParkedDTO parkedDTO)
        {
            if (parkedDTO == null)
            {
                return BadRequest();
            }
            try
            {
                Parked parked = _service.Create(parkedDTO);
                return CreatedAtRoute("GetParked", new { id = parked.Id }, parked);
            } 
            catch(Exception e)
            {                
                return BadRequest(e.Message);
            }
        }
    }
}