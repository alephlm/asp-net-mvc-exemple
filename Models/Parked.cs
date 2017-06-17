using System;
using Newtonsoft.Json;

namespace Invoicing.Models
{
    public class Parked
    {
        public long Id { get; set; }
        public Parking Parking { get; set; }
        [JsonIgnore]
        public Customer Customer {get; set; }
        public DateTime inTime { get; set; }
        public DateTime outTime { get; set; }
        public double Value { get; set; }
    }
}