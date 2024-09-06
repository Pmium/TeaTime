using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.DataAccess;
using TeaTime.Api.Domain.Orders;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores/{storeId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _service;


        public OrdersController(TeaTimeContext context, ILogger<OrdersService> logger)
        {
            _service = new OrdersService(context, logger);
        }

        // GET: api/stores/1/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders(long storeId)
        {
            var orders = _service.GetOrders(storeId);

            return Ok(orders);
        }

        // GET: api/stores/1/orders/1
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(long storeId, long id)
        {
            var order = _service.GetOrder(storeId, id);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/stores/1/orders
        [HttpPost]
        public IActionResult AddOrder(long storeId, [FromBody] OrderForCreation newOrder)
        {
            // 先檢查商家是否存在
            var isStoreExist = _service.IsStoreExist(storeId);
            if (!isStoreExist)
            {
                return BadRequest("無法新增訂單，請與維護人員聯繫");
            }

            var orderForReturn = _service.AddOrderAndReturn(storeId, newOrder);

            return CreatedAtAction(nameof(GetOrder), new { storeId, id = orderForReturn.Id }, orderForReturn);
        }
    }
}