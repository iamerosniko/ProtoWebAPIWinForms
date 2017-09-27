using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ProductController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        //Get All the products
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        //Add a product
        public Product PostProduct(Product item)
        {
            return repository.Add(item);
        }

        //Update a product
        public IEnumerable<Product> PutProduct(int id, Product product)
        {
            product.Id = id;
            if (repository.Update(product))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }

        //Delete a product
        public bool DeleteProduct(int id)
        {
            if (repository.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
