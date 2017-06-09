using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CodeTest.Models;
using System.IO;

namespace CodeTest.Db
{
    public class DbInterface
    {
        public List<Product> GetProducts()
        {
            // would have retrieved additional required columns if time allowed
            var helper = new DbHelper<Product>("SELECT Name FROM [Production].[Product]");

            helper.Mapper = getProduct;

            var products = helper.Get();

            return products;
        }

        public Product getProduct(SqlDataReader reader)
        {
            // would have mapped additional columns here
            var product = new Product();
            product.Name = reader.GetString(0);
            return new Product();
        }

    }
}