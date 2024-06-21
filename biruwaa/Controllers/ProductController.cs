using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biruwaa.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository db, ICategoryRepository categoryRepository)
        {
            _productRepository = db;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Product> objList = _productRepository.GetAll().ToList();
            return View(objList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(obj);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            TempData["Error"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()[0];
            return View(obj);
        }


    }
}
