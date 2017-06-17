using System;
using System.Collections.Generic;
using System.Linq;
using Invoicing.Models;

namespace TodoApi.Services
{
    public class ParkedService : IParkedService
    {
        private readonly InvoicingContext _context;

        public ParkedService (InvoicingContext context)
        {
            _context = context;
        }

        public Parked Create(ParkedDTO parkedDTO)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.Id == parkedDTO.customerId);
            Parking parking = _context.Parkings.FirstOrDefault(p => p.Id == parkedDTO.parkingId);

            if(customer == null || parking == null)
            {
                throw new Exception("You need to provide valid customerId and parkingId to create a Parked item.");
            }

            Parked parked = new Parked();
            parked.Customer = customer;
            parked.Parking = parking;
            parked.inTime = parkedDTO.inTime;
            parked.outTime = parkedDTO.outTime;

            TimeSpan timeSpent = parked.outTime - parked.inTime;
            int halfHours = (int)Math.Ceiling(timeSpent.TotalMinutes / 30);

            if(parked.inTime.Hour >= 7 && parked.inTime.Hour < 19)
            {
                parked.Value = customer.Type == 0 ? halfHours * 1.5 : halfHours;
            }
            else
            {
                parked.Value = customer.Type == 0 ? halfHours : halfHours * 0.75;
            }
            
            _context.Parkeds.Add(parked);
            _context.SaveChanges();

            return parked;  
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parked> GetAll()
        {
            return _context.Parkeds.ToList();  
        }

        public Parked GetById(long id)
        {
            return _context.Parkeds.FirstOrDefault(t => t.Id == id);  
        }

        public bool Update(long id, Parked parked)
        {
            throw new NotImplementedException();
        }
    }
}