using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Domain.Stores;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoresService _service;

        public StoresController(IStoresService service)
        {
            _service = service;
        }

        // GET: api/stores
        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores()
        {
            var stores = _service.GetStores();

            return Ok(stores);
        }

        // GET: api/stores/1
        [HttpGet("{id}")]
        public ActionResult<Store> GetStore(long id)
        {
            var store = _service.GetStoreAndReturn(id);

            if (store is null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // POST: api/stores
        [HttpPost]
        public IActionResult AddStore(StoreForCreation newStore)
        {
            var storeForReturn = _service.AddStore(newStore);

            return CreatedAtAction(nameof(GetStore), new { id = storeForReturn.Id }, storeForReturn);
        }
    }
}