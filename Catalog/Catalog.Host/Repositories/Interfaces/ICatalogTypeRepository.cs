using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> AddAsync(string name);
        Task<ItemsList<CatalogType>> GetTypesAsync();
        Task<bool> RemoveAsync(string name);
        Task<bool> UpdateAsync(string oldName, string newName);
    }
}
