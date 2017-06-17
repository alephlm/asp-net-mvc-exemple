using System;
using System.Collections.Generic;
using System.Linq;
using Invoicing.Models;

namespace TodoApi.Services
{
    public class ParkingService : IParkingService
    {
        private readonly InvoicingContext _context;

        public ParkingService(InvoicingContext context)
        {
            _context = context;

            if (_context.Parkings.Count() == 0)
            {
                _context.Parkings.Add(new Parking { Name = "Parking one"});
                _context.SaveChanges();
            }
        }

        public Parking Create(Parking parking)
        {
            _context.Parkings.Add(parking);
            _context.SaveChanges();
            return parking;        
        }

        public IEnumerable<Parking> GetAll()
        {
            return _context.Parkings.ToList();        
        }

        public Parking GetById(long id)
        {
            return _context.Parkings.FirstOrDefault(t => t.Id == id);        
        }

        public bool Update(long id, Parking parking)
        {
            throw new NotImplementedException();
        }

        bool IParkingService.Delete(long id)
        {
            var parking = _context.Parkings.First(t => t.Id == id);
            _context.Parkings.Remove(parking);
            _context.SaveChanges();
            return parking != null ? true : false;
        }
    }
}