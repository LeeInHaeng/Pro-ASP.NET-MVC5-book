using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SportsStore.Domain.Concrete
{
    public class DapperRepository : IProductRepository
    {
        private IDbConnection db = new SqlConnection(
          ConfigurationManager.ConnectionStrings[
              "ConnectionString"].ConnectionString);

        public IEnumerable<Product> Products
        {
            get
            {
                /*
                string sql = "SELECT * FROM Products";
                return this.db.Query<Product>(sql);
                */
                return this.db.Query<Product>("GetProducts");
            }
        }

        public void SaveProduct(Product product)
        {
            this.db.Query("UpdateProduct",
                new
                {
                    ProductID = product.ProductID,
                    NAME = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    ImageData = product.ImageData,
                    ImageMimeType = product.ImageMimeType
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
