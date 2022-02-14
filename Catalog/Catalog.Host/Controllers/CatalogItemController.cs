using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateProductRequest request)
    {
        var result = await _catalogItemService.AddAsync(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveItemResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(RemoveProductRequest request)
    {
        var result = await _catalogItemService.RemoveAsync(request.Name);
        return Ok(new RemoveItemResponse() { IsRemoved = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateProductRequest request)
    {
        var result = await _catalogItemService.UpdateAsync(request.OldName, request.NewName, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new UpdateItemResponse() { IsUpdated = result });
    }
}