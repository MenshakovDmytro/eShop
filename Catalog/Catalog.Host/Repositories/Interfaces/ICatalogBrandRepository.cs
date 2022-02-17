using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> AddAsync(string name);
        Task<int?> RemoveAsync(int id);
        Task<int?> UpdateAsync(int id, string name);
        Task<ItemsList<CatalogBrand>> GetBrandsAsync();
    }
}
