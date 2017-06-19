using System.Collections.Generic;
using Invoicing.Models;

namespace Invoicing.Services
{
    public interface IParkingService
    {
        IEnumerable<Parking> GetAll();
        Parking GetById(long id);
        Parking Create(Parking parking);
        bool Delete(long id);
        bool Update(long id, Parking parking);
    }
}