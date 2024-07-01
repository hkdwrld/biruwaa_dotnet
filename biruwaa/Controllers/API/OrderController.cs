using Biruwaa.DataAccess.Repository.IRepository;
using Biruwaa.Models;
using Biruwaa.Models.ViewModels.Order;
using Biruwaa.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biruwaa.Controllers.API
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IProductRepository _productRepository;
        public OrderController(IOrderRepository orderRepository, UserManager<AuthUser> userManager, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<OrderVM>> GetOrders()
        {
            //check if authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var name = User.GetUsername();

            // return all orders
            IEnumerable<Order> orders = await _orderRepository.GetAllAsync(u => u.AuthUser.UserName == name);
            IEnumerable<OrderVM> orderVMs = orders.Select(order => new OrderVM
            {
                ProductId = order.ProductId,
                AuthUserId = order.AuthUserId,
                Quantity = order.Quantity,
                Total = order.Total
            });
            return orderVMs;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<OrderVM> GetOrder(int id)
        {
            // return order by id
            Order order = await _orderRepository.GetFirstOrDefaultAsync(u => u.Id == id && u.AuthUser.UserName == User.GetUsername());
            if (order == null)
            {
                return null;
            }
            OrderVM orderVM = new OrderVM
            {
                ProductId = order.ProductId,
                AuthUserId = order.AuthUserId,
                Quantity = order.Quantity,
                Total = order.Total
            };
            return orderVM;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreateOrderVM order)
        {
            Console.WriteLine("Create Order");
            //check if authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var name = User.GetUsername();

            AuthUser authUser = await _userManager.FindByNameAsync(name);

            //check if user exists
            if (authUser == null)
            {
                return Unauthorized();
            }

            //check if product exists
            Product product = await _productRepository.GetFirstOrDefaultAsync(u => u.Id == order.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            // create new order
            if (ModelState.IsValid)
            {
                Order _order = new Order
                {
                    ProductId = order.ProductId,
                    AuthUserId = authUser.Id,
                    Quantity = order.Quantity,
                    Total = order.Total
                };
                _orderRepository.Add(_order);
                _orderRepository.Save();
                return Ok(order);
            }
            return BadRequest(ModelState);
        }
    }
}
