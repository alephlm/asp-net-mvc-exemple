using System.Collections.Generic;
using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using Invoicing.Services;

namespace Invoicing.Controllers
{
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService context)
        {
            _service = context;
        }
        
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var customer = _service.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _service.Create(customer);
            
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpGet("newinvoice/{id}")]
        public IActionResult GenerateInvoice(long id)
        {
            var invoice = _service.GenerateInvoice(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return new ObjectResult(invoice);
        }
    }
}