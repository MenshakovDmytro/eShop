using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> RemoveAsync(string name);
    Task<bool> UpdateAsync(string oldName, string newName, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> GetByIdAsync(int id);
    Task<ItemsList<CatalogItem>> GetByBrandAsync(string brand);
    Task<ItemsList<CatalogItem>> GetByTypeAsync(string type);
}