using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DbEntities;
using TeaTime.Api.Domain.Orders;

namespace TeaTime.Api.Services
{
    public class OrdersService
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(TeaTimeContext context, ILogger<OrdersService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Order> GetOrders(long storeId)
        {
            var results = _context.Orders.Where(o => o.StoreId == storeId).ToList();

            var orders = new List<Order>();
            foreach (var result in results)
            {
                orders.Add(new Order
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    ItemName = result.ItemName,
                    Price = 0 // TODO: 從商品資料表中取得價格
                });
            }

            return orders;
        }

        public Order? GetOrder(long storeId, long id)
        {
            // 先檢查商家是否存在
            if (!IsStoreExist(storeId))
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", storeId);
                return null;
            }

            // 再檢查訂單是否存在且屬於該商家
            var result = _context.Orders.Find(id);

            if (result is null || result.StoreId != storeId)
            {
                _logger.LogWarning("訂單代號 {id} 不存在或不屬於商家", id);
                return null;
            }

            var order = new Order
            {
                Id = result.Id,
                UserName = result.UserName,
                ItemName = result.ItemName,
                Price = 0 // TODO: 從商品資料表中取得價格
            };

            return order;
        }

        public Order AddOrderAndReturn(long storeId, OrderForCreation newOrder)
        {
            var maxId = _context.Orders.Any() ? _context.Orders.Max(o => o.Id) : 0;
            var entity = new OrderEntity
            {
                Id = maxId + 1,
                StoreId = storeId,
                UserName = newOrder.UserName,
                ItemName = newOrder.ItemName
            };

            _context.Orders.Add(entity);
            _context.SaveChanges();

            var order = new Order
            {
                Id = entity.Id,
                UserName = entity.UserName,
                ItemName = entity.ItemName,
                Price = 0 // TODO: 從商品資料表中取得價格
            };

            return order;
        }

        public bool IsStoreExist(long storeId)
        {
            var isStoreExists = _context.Stores.Find(storeId) != null;

            if (!isStoreExists)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在，無法新增訂單", storeId);
            }

            return isStoreExists;
        }
    }
}