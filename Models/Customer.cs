using System.Collections.Generic;

namespace Invoicing.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CustomerType Type { get; set; }
        public ICollection<Parked> Parkeds { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}