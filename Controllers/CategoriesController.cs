using ExamenAPIEFC.Models;
using ExamenAPIEFC.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamenAPIEFC.Responses;
using Microsoft.EntityFrameworkCore;

namespace ExamenAPIEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public List<CategoryResponseDTO> Get()
        {
            using (var context = new AppDbContext())
            {
                List<Category> categories = new List<Category>();
                categories = context.Categories.ToList();
                List<CategoryResponseDTO> response = new List<CategoryResponseDTO>();
                foreach (var category in categories)
                {
                    response.Add(new CategoryResponseDTO
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.CategoryName,
                        CategoryDescription = category.CategoryDescription,
                    });
                }
                return response;
            }
        }

        [HttpGet]
        public List<Category> Get2()
        {
            using (var context = new AppDbContext())
            {
                var categories = context.Categories.Include(x=> x.Products).ToList();


                return categories;
            }
        }

        [HttpGet]
        public CategoryResponseDTO GetById(int _ID)
        {
            using (var context = new AppDbContext())
            {
                Category category = new Category();
                category = context.Categories.Find(_ID);
                CategoryResponseDTO response = new CategoryResponseDTO
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                };
                return response;
            }
        }

        [HttpGet]
        public CategoryResponseDTO GetById2(int _ID)
        {
            using (var context = new AppDbContext())
            {
                var response = context.Categories.Where(y => y.CategoryID == _ID).Select(x => new CategoryResponseDTO
                {
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName,
                    CategoryDescription = x.CategoryDescription
                }).FirstOrDefault();
                return response;
            }
        }

        [HttpGet]
        public List<CategoryResponseDTO> Get3()
        {
            using (var context = new AppDbContext())
            {                
                var response = context.Categories.Select(x=> new CategoryResponseDTO 
                {
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName,
                    CategoryDescription = x.CategoryDescription
                }).ToList();

                return response;
            }
        }

        [HttpPost]
        public Response Insert(CategoryRequestDTO _request)
        {
            using (var context = new AppDbContext())
            {
                Response response = new Response();
                try
                {
                    Category category = new Category
                    {
                        CategoryName = _request.CategoryName,
                        CategoryDescription = _request.CategoryDescription
                    };
                    context.Categories.Add(category);
                    context.SaveChanges();
                    response.ResponseCode = 200;
                    response.ResponseMessage = "OK";
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
}
