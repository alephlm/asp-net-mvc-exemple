using System.Collections.Generic;
using Invoicing.Models;

namespace Invoicing.Services
{
    public interface IParkedService
    {
        IEnumerable<Parked> GetAll();
        Parked GetById(long id);
        Parked Create(ParkedDTO parkedDTO);
        bool Delete(long id);
        bool Update(long id, Parked parked);

    }
}