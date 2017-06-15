using System.Collections.Generic;
using Invoicing.Models;

namespace TodoApi.Services
{
    public interface IParkedService
    {
        IEnumerable<Parked> GetAll();
        Parked GetById(long id);
        Parked Create(Parked parked);
        bool Delete(long id);
        bool Update(long id, Parked parked);
    }
}