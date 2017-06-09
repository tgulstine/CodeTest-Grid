using System.Web.Http;
using CodeTest.Db;
using CodeTest.Models;
using System.Collections.Generic;

namespace CodeTest.Controllers
{
    public class ProductController : ApiController
    {
        // GET: Product
        public List<Product> Get()
        {
            var dbInterface = new DbInterface();
            var products = dbInterface.GetProducts();
            return products;
        }
    }
}