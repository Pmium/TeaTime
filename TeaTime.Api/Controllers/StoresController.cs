using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.DataAccess;
using TeaTime.Api.Domain.Stores;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly StoresService _service;
        private readonly ILogger _logger;

        public StoresController(TeaTimeContext context, ILogger<StoresService> logger)
        {
            _service = new StoresService(context, logger);
            _logger = logger;
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