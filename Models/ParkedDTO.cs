using System;

namespace Invoicing.Models
{
    public class ParkedDTO
    {
        public long parkingId { get; set; }
        public long customerId {get; set;}
        public DateTime inTime { get; set; }
        public DateTime outTime { get; set; }

    }
}