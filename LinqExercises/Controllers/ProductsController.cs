﻿using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class ProductsController : ApiController
    {
        private NORTHWNDEntities _db;

        public ProductsController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/products/discontinued/count
        [HttpGet, Route("api/products/discontinued/count"), ResponseType(typeof(int))]
        public IHttpActionResult GetDiscontinuedCount()
        {
            //("Write a query to return the number of discontinued products in the Products table.");
            var query = _db.Products.Count(p => p.Discontinued == true);

            return Ok(query);
        }

        // GET: api/categories/Condiments/products
        [HttpGet, Route("api/categories/{categoryName}/products"), ResponseType(typeof(IQueryable<Product>))]
        public IHttpActionResult GetProductsInCategory(string categoryName)
        {
            //("Write a query to return all products that fall within the given categoryName.");
            //("Write a query to return all customers with orders that shipped using the given shipperName.");

            var getCategoryName = from p in _db.Products
                                  join c in _db.Categories on p.CategoryID equals c.CategoryID
                                  where c.CategoryName == categoryName
                                  select p;
            return Ok(getCategoryName);
        }

        // GET: api/products/reports/stock
        [HttpGet, Route("api/products/reports/stock"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult GetStockReport()
        {
            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/
            //throw new NotImplementedException(@"
            //    Write a query to return an array of anonymous objects that have two properties. 

            //    1. A Product property containing that particular product
            //    2. A TotalStockUnits property containing the total amount of stock for that particular product. (UnitsInStock + UnitsOnOrder)

            //    Only return rows where TotalStockUnits is greater than 100.
            //");

            var product = from p in _db.Products
                          select
                          new { property = p,
                                totalStockUnits = p.UnitsInStock + p.UnitsOnOrder };

            var TotalStock100 = from p in product
                                where p.totalStockUnits > 100
                                select p;
                            
                         
                          


            return Ok(TotalStock100);
            

        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
