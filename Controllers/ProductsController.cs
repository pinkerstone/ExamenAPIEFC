using ExamenAPIEFC.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamenAPIEFC.Models;
using ExamenAPIEFC.Requests;
using Microsoft.EntityFrameworkCore;

namespace ExamenAPIEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public List<ProductResponseDTO> Get2()
        {
            using (var context = new AppDbContext())
            {
                List<Product> products = new List<Product>();
                products = context.Products.ToList();
                List<Category> categories = new List<Category>();
                categories = context.Categories.ToList();
                var response = products.Join(
                    categories,                       // Tabla a unir
                    p => p.CategoryID,               // Clave de productos
                    c => c.CategoryID,                        // Clave de categorías
                    (p, c) => new ProductResponseDTO                   // Resultado del join
                        {
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                            ProductPrice = p.ProductPrice,
                            CategoryName = c.CategoryName
                        }).ToList();
                return response;
            }
        }

        [HttpGet]
        public List<ProductResponseDTO> Get()
        {
            using (var context = new AppDbContext())
            {
                var response = context.Products.Include(p => p.Category)
                                                .Select(x => new ProductResponseDTO
                                                            { 
                                                                ProductID = x.ProductID,
                                                                ProductName = x.ProductName,
                                                                ProductPrice = x.ProductPrice,
                                                                CategoryName = x.Category.CategoryName
                                                            }).ToList();    

                return response;
                
            }
        }
        
        [HttpGet]
        public ProductResponseDTO GetById(int _ID)
        {
            using (var context = new AppDbContext())
            {
                var response = context.Products.Include(p=> p.Category)
                                                .Where(x=> x.ProductID == _ID)
                                                .Select(n=> new ProductResponseDTO
                                                    {
                                                        ProductID = n.ProductID,
                                                        ProductName = n.ProductName,
                                                        ProductPrice = n.ProductPrice,
                                                        CategoryName = n.Category.CategoryName
                                                    }).FirstOrDefault();
                
                return response;
            }
        }

        [HttpPost]
        public Response Insert(ProductRequestDTO _request)
        {
            Response response = new Response();
            try
            {
                using (var context = new AppDbContext())
                {
                    Product product = new Product
                    {
                        ProductName = _request.ProductName,
                        ProductPrice = _request.ProductPrice,
                        CategoryID = _request.CategoryId
                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                    response.ResponseCode = 200;
                    response.ResponseMessage = "OK";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
    }
}
