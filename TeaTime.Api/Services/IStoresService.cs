using TeaTime.Api.Domain.Stores;

namespace TeaTime.Api.Services
{
    public interface IStoresService
    {
        IEnumerable<Store> GetStores();

        Store? GetStoreAndReturn(long id);

        Store AddStore(StoreForCreation newStore);

        bool IsStoreExist(long id);
    }
}
