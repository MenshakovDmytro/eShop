using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateBrandRequest request)
    {
        var result = await _catalogBrandService.AddAsync(request.Name);
        return Ok(new AddBrandResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveBrandRequest request)
    {
        var result = await _catalogBrandService.RemoveAsync(request.Id);
        return Ok(new RemoveBrandResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.UpdateAsync(request.Id, request.Name);
        return Ok(new UpdateBrandResponse<int?>() { Id = result });
    }
}