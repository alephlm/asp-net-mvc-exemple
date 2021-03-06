using System;
using System.Collections.Generic;
using System.Linq;
using Invoicing.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly InvoicingContext _context;

        public CustomerService(InvoicingContext context)
        {
            _context = context;
            if (_context.Customers.Count() == 0)
            {
                _context.Customers.Add(new Customer { Name = "Regular Customer", Type = CustomerType.regular });
                _context.Customers.Add(new Customer { Name = "Premium Customer", Type = CustomerType.premium });
                _context.SaveChanges();
            }
        }

        public Customer Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Invoice GenerateInvoice(long id)
        {
            Customer customer = _context.Customers.Where(x => x.Id == id)
                                                .Include(c => c.Parkeds)
                                                .Include(c => c.Invoices)
                                                .FirstOrDefault();
            //Gets all Parked items that is not present in any Invoice.
            List<Parked> noInvoicedParkeds = customer.Parkeds.Except(customer.Invoices.SelectMany(i => i.Parkeds)).ToList();
            //Premium customer was charged this month?
            bool anyChargedThisMonth = customer.Type == 0 ? true : customer.Invoices.LastOrDefault() != null && customer.Invoices.LastOrDefault().Created.Month == DateTime.Now.Month;
            if(noInvoicedParkeds.Count > 0)
            {
                Invoice invoice = new Invoice();
                invoice.Created = DateTime.Now;
                invoice.Customer = customer;
                invoice.Parkeds = noInvoicedParkeds;
                invoice.MonthlyFee = anyChargedThisMonth ? 0 : 20;
                invoice.Total = noInvoicedParkeds.Sum(i => i.Value) + invoice.MonthlyFee;
                if(customer.Type == CustomerType.premium && invoice.Total > 300)
                {
                    invoice.Total = 300;
                }
                
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return invoice;
            } 
            else
            {
                return new Invoice();
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(long id)
        {
            return _context.Customers.Where(x => x.Id == id)
                                     .Include(c => c.Parkeds)
                                        .ThenInclude(p => p.Parking)
                                     .Include(c => c.Invoices)
                                     .FirstOrDefault();
        }

        public bool Update(long id, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}