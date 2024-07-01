using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;
using Biruwaa.Models.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Biruwaa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _productRepository.GetAllAsync();
            var productVMList = productList.Select(u => new ProductVM
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                ImageUrl = u.ImageUrl,
                Price = u.Price,
                Category = _categoryRepository.GetFirstOrDefault(c => c.Id == u.CategoryId).Name
            });
            ViewBag.Products = productVMList;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productRepository.GetFirstOrDefaultAsync(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = _categoryRepository.GetFirstOrDefault(u => u.Id == product.CategoryId).Name
            };
            return View(productVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
