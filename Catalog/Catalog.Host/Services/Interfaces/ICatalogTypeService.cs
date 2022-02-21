using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<ItemsListResponse<CatalogTypeDto>> GetTypesAsync();
    }
}
