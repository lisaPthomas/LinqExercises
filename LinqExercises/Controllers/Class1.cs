using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqExercises.Controllers
{
    public class Class1
    {
    }
}



// GET: api/Properties/search
[Route("api/Properties/search")]
[HttpGet]
public IQueryable<Property> SearchProperties([FromUri]PropertySearch objectSearch)
{
    IQueryable<Property> properties = db.Properties;

    if (!String.IsNullOrEmpty(objectSearch.City))
    {
        properties = properties.Where(p => p.City == objectSearch.City);
    }

    if (!String.IsNullOrEmpty(objectSearch.Zip))
    {
        properties = properties.Where(p => p.Zip == objectSearch.Zip);
    }

    if (!String.IsNullOrEmpty(objectSearch.minRent.ToString()))
    {
        properties = properties.Where(p => p.Rent >= objectSearch.minRent);
    }

    if (!String.IsNullOrEmpty(objectSearch.maxRent.ToString()))
    {
        properties = properties.Where(p => p.Rent <= objectSearch.maxRent);
    }

    return properties;

}
