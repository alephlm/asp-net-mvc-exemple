using System.Collections.Generic;
using Invoicing.Models;
using Microsoft.AspNetCore.Mvc;
using Invoicing.Services;
using Microsoft.Extensions.Logging;

namespace Invoicing.Controllers
{
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService context, ILogger<CustomerController> logger)
        {
            _service = context;
            _logger = logger;
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
                _logger.LogWarning(0, "||| Customer not found |||");
                return NotFound();
            }
            _logger.LogInformation(0, "||| Retrieved customer: {c} |||", customer.Name);
            return new ObjectResult(customer);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            if (customer == null)
            {
                _logger.LogError(0, "||| Error on create customer |||");
                return BadRequest();
            }

            _service.Create(customer);
            _logger.LogInformation(0, "||| Created customer: {c} |||", customer.Name);
    
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpGet("newinvoice/{id}")]
        public IActionResult GenerateInvoice(long id)
        {
            var invoice = _service.GenerateInvoice(id);
            if (invoice == null)
            {
                _logger.LogError(0, "||| Error on generate invoice |||");
                return BadRequest();
            }
            if (invoice.Id > 0)
                _logger.LogInformation(0, "||| Created invoice of: ${c} |||", invoice.Total);
            return new ObjectResult(invoice);
        }
    }
}