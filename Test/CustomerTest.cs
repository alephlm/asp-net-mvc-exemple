using System.Collections.Generic;
using Invoicing.Models;
using Invoicing.Services;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;

namespace Invoicing.Test.Services
{
    public class CustomerTest
    {
        private readonly ICustomerService _customerService;
        private readonly IParkingService _parkingService;
        private readonly IParkedService _parkedService;
        private readonly InvoicingContext _invoicingContext;

        public CustomerTest()
        {
            var builder = new DbContextOptionsBuilder<InvoicingContext>();
            builder.UseInMemoryDatabase($"database{Guid.NewGuid()}");
            _invoicingContext = new InvoicingContext(builder.Options);
            _customerService = new CustomerService(_invoicingContext);
            _parkingService = new ParkingService(_invoicingContext);
            _parkedService = new ParkedService(_invoicingContext);
        }

        [Fact]
        public void TwoDefaultCustomersPresentInDB()
        {
            List<Customer> customers = _customerService.GetAll().ToList();
            Assert.Equal(2, customers.Count());
        }

        [Fact]
        public void GenerateInvoice()
        {
            long customerId = _customerService.GetAll().FirstOrDefault().Id;
            long parkingId = _parkingService.GetAll().FirstOrDefault().Id;

            ParkedDTO p = new ParkedDTO();
            p.inTime = DateTime.Now;
            p.outTime = DateTime.Now.AddHours(3);
            p.customerId = customerId;
            p.parkingId = parkingId;
            
            _parkedService.Create(p);
            _customerService.GenerateInvoice(customerId);
            var invoices = _customerService.GetById(customerId).Invoices.Count();
            
            Assert.Equal(1, invoices);
        }

        [Fact]
        public void NotGeneratesInvoice()
        {
            Customer customer = _customerService.GetAll().FirstOrDefault();

            _customerService.GenerateInvoice(customer.Id);
            var invoices = _customerService.GetById(customer.Id).Invoices.Count();

            Assert.Equal(0, invoices);
        }

        [Fact]
        public void GenerateInvoice11EUR()
        {
            long customerId = _customerService.GetAll().FirstOrDefault().Id;
            long parkingId = _parkingService.GetAll().FirstOrDefault().Id;

            DateTime eight12 =      new DateTime(2017, 6, 20, 8, 12, 0);
            DateTime ten45 =        new DateTime(2017, 6, 20, 10, 45, 0);

            DateTime nineteen40 =   new DateTime(2017, 6, 20, 19, 40, 0);
            DateTime twenty35 =     new DateTime(2017, 6, 20, 20, 35, 0);

            ParkedDTO p1 = new ParkedDTO();
            p1.inTime = eight12;
            p1.outTime = ten45;
            p1.customerId = customerId;
            p1.parkingId = parkingId;
            _parkedService.Create(p1);

            ParkedDTO p2 = new ParkedDTO();
            p2.inTime = nineteen40;
            p2.outTime = twenty35;
            p2.customerId = customerId;
            p2.parkingId = parkingId;
            _parkedService.Create(p2);

            _customerService.GenerateInvoice(customerId);
            var invoice = _customerService.GetById(customerId).Invoices.FirstOrDefault();
            
            Assert.Equal(11, invoice.Total);
        }

        [Fact]
        public void GenerateInvoice38_25EUR()
        {
            long customerId = _customerService.GetAll().Where(c => c.Type == CustomerType.premium).FirstOrDefault().Id;
            long parkingId = _parkingService.GetAll().FirstOrDefault().Id;

            DateTime eight12 =    new DateTime(2017, 6, 20, 8, 12, 0);
            DateTime ten45 =      new DateTime(2017, 6, 20, 10, 45, 0);

            DateTime nineteen40 = new DateTime(2017, 6, 20, 19, 40, 0);
            DateTime twenty35 =   new DateTime(2017, 6, 20, 20, 35, 0);

            DateTime seven02 =    new DateTime(2017, 6, 20, 7, 02, 0);
            DateTime eleven56 =   new DateTime(2017, 6, 20, 11, 56, 0);

            DateTime twntytwo10 = new DateTime(2017, 6, 20, 22, 10, 0);
            DateTime twntytwo35 = new DateTime(2017, 6, 20, 22, 35, 0);

            ParkedDTO p1 = new ParkedDTO();
            p1.inTime = eight12;
            p1.outTime = ten45;
            p1.customerId = customerId;
            p1.parkingId = parkingId;
            _parkedService.Create(p1);

            ParkedDTO p2 = new ParkedDTO();
            p2.inTime = nineteen40;
            p2.outTime = twenty35;
            p2.customerId = customerId;
            p2.parkingId = parkingId;
            _parkedService.Create(p2);

            ParkedDTO p3 = new ParkedDTO();
            p3.inTime = seven02;
            p3.outTime = eleven56;
            p3.customerId = customerId;
            p3.parkingId = parkingId;
            _parkedService.Create(p3);
                    
            ParkedDTO p4 = new ParkedDTO();
            p4.inTime = twntytwo10;
            p4.outTime = twntytwo35;
            p4.customerId = customerId;
            p4.parkingId = parkingId;
            _parkedService.Create(p4);

            _customerService.GenerateInvoice(customerId);
            var invoice = _customerService.GetById(customerId).Invoices.FirstOrDefault();
            
            Assert.Equal(38.25, invoice.Total);
        }

        [Fact]
        public void PremiumCustomerChargedOnlyOncePerMonth()
        {
            long customerId = _customerService.GetAll().Where(c => c.Type == CustomerType.premium).FirstOrDefault().Id;            
            long parkingId = _parkingService.GetAll().FirstOrDefault().Id;

            DateTime eight12 =      new DateTime(2017, 6, 20, 8, 12, 0);
            DateTime ten45 =        new DateTime(2017, 6, 20, 10, 45, 0);

            DateTime nineteen40 =   new DateTime(2017, 6, 20, 19, 40, 0);
            DateTime twenty35 =     new DateTime(2017, 6, 20, 20, 35, 0);

            ParkedDTO p1 = new ParkedDTO();
            p1.inTime = eight12;
            p1.outTime = ten45;
            p1.customerId = customerId;
            p1.parkingId = parkingId;
            _parkedService.Create(p1);

            _customerService.GenerateInvoice(customerId);
            var fistInvoice = _customerService.GetById(customerId).Invoices.LastOrDefault();

            Assert.Equal(20, fistInvoice.MonthlyFee);
            
            ParkedDTO p2 = new ParkedDTO();
            p2.inTime = nineteen40;
            p2.outTime = twenty35;
            p2.customerId = customerId;
            p2.parkingId = parkingId;
            _parkedService.Create(p2);

            _customerService.GenerateInvoice(customerId);
            var secondInvoice = _customerService.GetById(customerId).Invoices.LastOrDefault();
            
            Assert.Equal(0, secondInvoice.MonthlyFee);
        }

        [Fact]
        public void PremiumCustomerChargedNoMoreThan300()
        {
            long customerId = _customerService.GetAll().Where(c => c.Type == CustomerType.premium).FirstOrDefault().Id;            
            long parkingId = _parkingService.GetAll().FirstOrDefault().Id;

            DateTime eight12 =      new DateTime(2017, 6, 20, 8, 12, 0);
            DateTime ten45 =        new DateTime(2017, 9, 20, 10, 45, 0);

            ParkedDTO p1 = new ParkedDTO();
            p1.inTime = eight12;
            p1.outTime = ten45;
            p1.customerId = customerId;
            p1.parkingId = parkingId;
            _parkedService.Create(p1);

            _customerService.GenerateInvoice(customerId);
            var invoice = _customerService.GetById(customerId).Invoices.LastOrDefault();
            
            Assert.Equal(300, invoice.Total);
        }
    }
}