using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> RemoveAsync(int id);
    Task<int?> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItemDto?> GetByIdAsync(int id);
    Task<ItemsListResponse<CatalogItemDto>> GetByBrandAsync(string brand);
    Task<ItemsListResponse<CatalogItemDto>> GetByTypeAsync(string type);
}