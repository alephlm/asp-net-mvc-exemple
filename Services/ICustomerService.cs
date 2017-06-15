using System.Collections.Generic;
using Invoicing.Models;

namespace TodoApi.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(long id);
        Customer Create(Customer customer);
        bool Delete(long id);
        bool Update(long id, Customer customer);
    }
}