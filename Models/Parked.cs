namespace Invoicing.Models
{
    public class Parked
    {
        public long Id { get; set; }
        public Parking Parking { get; set; }
        public Customer Customer {get; set;}
    }
}