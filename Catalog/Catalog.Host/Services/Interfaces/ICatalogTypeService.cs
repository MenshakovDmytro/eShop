using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> AddAsync(string name);
        Task<ItemsListResponse<CatalogTypeDto>> GetTypesAsync();
        Task<bool> RemoveAsync(string name);
        Task<bool> UpdateAsync(string oldName, string newName);
    }
}
