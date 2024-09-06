using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DbEntities;
using TeaTime.Api.Domain.Stores;

namespace TeaTime.Api.Services
{
    public class StoresService : IStoresService
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<StoresService> _logger;

        public StoresService(TeaTimeContext context, ILogger<StoresService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Store> GetStores()
        {
            var results = _context.Stores.ToList();

            var stores = new List<Store>();
            foreach (var result in results)
            {
                stores.Add(new Store
                {
                    Id = result.Id,
                    Name = result.Name,
                    PhoneNumber = result.PhoneNumber,
                    MenuUrl = result.MenuUrl
                });
            }

            return stores;
        }

        public Store? GetStoreAndReturn(long id)
        {
            var result = _context.Stores.Find(id);

            if (result is null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", id);
                return null;
            }

            var store = new Store
            {
                Id = result.Id,
                Name = result.Name,
                PhoneNumber = result.PhoneNumber,
                MenuUrl = result.MenuUrl
            };

            return store;
        }

        public Store AddStore(StoreForCreation newStore)
        {
            var maxId = _context.Stores.Any() ? _context.Stores.Max(s => s.Id) : 0;
            var entity = new StoreEntity
            {
                Id = maxId + 1,
                Name = newStore.Name,
                PhoneNumber = newStore.PhoneNumber,
                MenuUrl = newStore.MenuUrl
            };

            _context.Stores.Add(entity);
            _context.SaveChanges();

            var storeForReturn = new Store
            {
                Id = entity.Id,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                MenuUrl = entity.MenuUrl
            };

            return storeForReturn;
        }

        public bool IsStoreExist(long id)
        {
            var isStoreExists = _context.Stores.Find(id) != null;

            if (!isStoreExists)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", id);
            }

            return isStoreExists;
        }
    }
}