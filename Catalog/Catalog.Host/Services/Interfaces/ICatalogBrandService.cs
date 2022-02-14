using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> AddAsync(string name);
        Task<bool> RemoveAsync(string name);
        Task<bool> UpdateAsync(string oldName, string newName);
        Task<ItemsListResponse<CatalogBrandDto>> GetBrandsAsync();
    }
}
