using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoicing.Models
{
    public class Invoice
    {
        public long Id { get; set; }
        [JsonIgnore]
        public Customer Customer {get; set;}
        public ICollection<Parked> Parkeds {get; set;}
        public DateTime Created { get; set; }
        public double MonthlyFee { get; set; }
        public double Total { get; set; }
    }
}