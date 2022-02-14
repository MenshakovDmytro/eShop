using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> AddAsync(string name);
        Task<bool> RemoveAsync(string name);
        Task<bool> UpdateAsync(string oldName, string newName);
        Task<ItemsList<CatalogBrand>> GetBrandsAsync();
    }
}
