﻿using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Models;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores/{storeId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(TeaTimeContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/stores/1/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders(long storeId)
        {
            var orders = _context.Orders.Where(o => o.StoreId == storeId).ToList();

            return Ok(orders);
        }

        // GET: api/stores/1/orders/1
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(long storeId, long id)
        {
            // 先檢查商家是否存在
            var store = _context.Stores.Find(storeId);
            if (store is null)
            {
                return NotFound();
            }


            // 再檢查訂單是否存在且屬於該商家
            var order = _context.Orders.Find(id);

            if (order is null || order.StoreId != storeId)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/stores/1/orders
        [HttpPost]
        public IActionResult AddOrder(long storeId, [FromBody] Order newOrder)
        {
            // 先檢查商家是否存在
            var store = _context.Stores.Find(storeId);
            if (store is null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在，無法新增訂單", storeId);
                return BadRequest("無法新增訂單，請與維護人員聯繫");
            }

            newOrder.StoreId = storeId;
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { storeId, id = newOrder.Id }, newOrder);
        }
    }
}
