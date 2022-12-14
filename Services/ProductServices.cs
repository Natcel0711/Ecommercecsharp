using Ecommercecsharp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using System.Linq;

namespace Ecommercecsharp.Services
{
    public class ProductServices
    {
        #region Private members
        private ProductDbContext dbContext;
        #endregion

        #region Constructor
        public ProductServices(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// This method returns the list of product
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetProductAsync()
        {
            return await dbContext.Product.ToListAsync();
        }

        /// <summary>
        /// This method add a new product to the DbContext and saves it
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                dbContext.Product.Add(product);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        /// <summary>
        /// This method update and existing product and saves the changes
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                var productExist = dbContext.Product.FirstOrDefault(p => p.Id == product.Id);
                if (productExist != null)
                {
                    dbContext.Update(product);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        /// <summary>
        /// This method removes and existing product from the DbContext and saves it
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task DeleteProductAsync(Product product)
        {
            try
            {
                dbContext.Product.Remove(product);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region [Fake Data API]
        public async Task<List<ProductAPI>> GetProdsAPI()
        {
            var jsonText = File.ReadAllText("D:\\code\\Ecommercecsharp\\dummydata\\json.json");
            var model = JsonConvert.DeserializeObject<List<ProductAPI>>(jsonText);
            return model;
        }
        public async Task<ProductAPI> GetProdByID(int id)
        {
            var prods = await GetProdsAPI();
            var prod = prods.Where(x => x.id == id).FirstOrDefault();
            return prod;
        }
        #endregion
    }
}
