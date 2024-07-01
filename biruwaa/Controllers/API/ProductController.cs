using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Biruwaa.Controllers.API
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductVM>> Get(int page = 1, int pageSize = 10)
        {
            var productList = await _productRepository.Got();

            var paginatedList = productList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return paginatedList.Select(product => new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = _categoryRepository.GetFirstOrDefault(c => c.Id == product.CategoryId).Name
            });
        }

        [HttpGet("{id}")]
        public async Task<ProductVM> Get(int id)
        {
            var product = await _productRepository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            var productVm = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = _categoryRepository.GetFirstOrDefault(c => c.Id == product.CategoryId)?.Name
            };
            return productVm;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<ProductVM>> Search(string keyword, int page = 1, int pageSize = 10)
        {
            var productList = await _productRepository.GetAllAsync(p => p.Name.Contains(keyword));

            var paginatedList = productList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return paginatedList.Select(product => new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = _categoryRepository.GetFirstOrDefault(c => c.Id == product.CategoryId).Name
            });
        }
    }
}
