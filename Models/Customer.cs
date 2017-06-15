using System.Collections.Generic;

namespace Invoicing.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CustomerType type { get; set; }
        public IEnumerable<Parked> parkeds { get; set;}
    }
}