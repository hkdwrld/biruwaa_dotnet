//using Biruwaa.DataAccess.Repository.IRepository;
//using Biruwaa.Models;
//using Biruwaa.Models.ViewModels.Category;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace Biruwaa.Controllers.API
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoryController : ControllerBase
//    {
//        private readonly ICategoryRepository _categoryRepository;
//        public CategoryController(ICategoryRepository categoryRepository)
//        {
//            _categoryRepository = categoryRepository;
//        }


//        // GET: api/<CategoryController>
//        [HttpGet]
//        public async Task<IEnumerable<Category>> Get()
//        {
//            return await _categoryRepository.GetAllAsync();
//        }

//        // GET api/<CategoryController>/5
//        [HttpGet("{id}")]
//        public async Task<Category> Get(int id)
//        {
//            return await _categoryRepository.GetFirstOrDefaultAsync(c => c.Id == id);
//        }


//        // POST api/<CategoryController>
//        [HttpPost]
//        public CategoryVM Post([FromBody] CategoryVM category)
//        {
//            if (ModelState.IsValid)
//            {
//                _categoryRepository.Add(
//                    new Category
//                    {
//                        Name = category.Name
//                    }
//                );
//                _categoryRepository.Save();
//            }
//            return category;
//        }

//        // PUT api/<CategoryController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<CategoryController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
