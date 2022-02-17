using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> AddAsync(string name);
        Task<ItemsList<CatalogType>> GetTypesAsync();
        Task<int?> RemoveAsync(int id);
        Task<int?> UpdateAsync(int id, string name);
    }
}
