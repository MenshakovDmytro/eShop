using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> AddAsync(string name);
        Task<ItemsListResponse<CatalogTypeDto>> GetTypesAsync();
        Task<int?> RemoveAsync(int id);
        Task<int?> UpdateAsync(int id, string name);
    }
}
