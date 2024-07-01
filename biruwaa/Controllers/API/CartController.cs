using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;
using Biruwaa.Models.ViewModels.Cart;
using Biruwaa.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biruwaa.Controllers.API
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<AuthUser> _userManager;

        public CartController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICartRepository cartRepository, UserManager<AuthUser> userManager)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<CartShowVM>> Get(int page = 1, int pageSize = 10)
        {
            var user = User.GetUsername();
            var authUser = await _userManager.FindByNameAsync(user);
            var cartList = await _cartRepository.Got(authUser.Id);

            var cartVMList = new List<CartShowVM>();
            foreach (var item in cartList)
            {
                var product = await _productRepository.GetFirstOrDefaultAsync(x => x.Id == item.ProductId);
                cartVMList.Add(new CartShowVM
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                    ProductName = product.Name,
                });
            }
            return cartVMList;

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CartVM> cart)
        {
            var user = User.GetUsername();
            var authUser = await _userManager.FindByNameAsync(user);

            foreach (var item in cart)
            {
                var product = await _productRepository.GetFirstOrDefaultAsync(x => x.Id == item.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                var cartObj = new Cart
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                    AuthUserId = authUser.Id
                };

                await _cartRepository.Add(cartObj, authUser.Id);
            }

            return Ok();
        }

    }
}
