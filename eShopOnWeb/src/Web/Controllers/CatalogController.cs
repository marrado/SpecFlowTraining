using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("")]
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page, int pageSize = Constants.DefaultCatalogPageSize)
        {          
            var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, pageSize, brandFilterApplied, typesFilterApplied);
            return View(catalogModel);
        }

        [HttpGet("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
