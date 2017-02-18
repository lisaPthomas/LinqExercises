using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            //("Write a query to return all customers in the given city");

            var citySearch = _db.Customers.Where(c => c.City == city);

            return Ok(citySearch);
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            //("Write a query to return all customers from Mexico, Sweden and Germany.");
            var countryMSG = _db.Customers.Where(c => c.Country == "Mexico" || c.Country == "Sweden" || c.Country == "Germany");
            return Ok(countryMSG);
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            //("Write a query to return all customers with orders that shipped using the given shipperName.");

            var getShipper =               from c in _db.Customers
                                           join o in _db.Orders on c.CustomerID equals o.CustomerID
                                           join s in _db.Shippers on o.ShipVia equals s.ShipperID
                                           where s.CompanyName == (shipperName)
                                           select c;
                           

            return Ok(getShipper.Distinct());
        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            //("Write a query to return all customers with no orders in the Orders table.");
            var query = from c in _db.Customers
                        where !(from o in _db.Orders select o.CustomerID)
                        .Contains(c.CustomerID)
                        select c;
            return Ok(query);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
