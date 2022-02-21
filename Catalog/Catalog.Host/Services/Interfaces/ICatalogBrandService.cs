using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<ItemsListResponse<CatalogBrandDto>> GetBrandsAsync();
    }
}
