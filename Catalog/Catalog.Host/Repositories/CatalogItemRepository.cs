using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price,
            AvailableStock = availableStock
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem?> GetByIdAsync(int id)
    {
        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstOrDefaultAsync(f => f.Id == id);

        return item;
    }

    public async Task<ItemsList<CatalogItem>> GetByBrandAsync(string brand)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(w => w.CatalogBrand.Brand.Equals(brand))
            .ToListAsync();

        return new ItemsList<CatalogItem>() { TotalCount = items.Count, Data = items };
    }

    public async Task<ItemsList<CatalogItem>> GetByTypeAsync(string type)
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Where(w => w.CatalogType.Type.Equals(type))
            .ToListAsync();

        return new ItemsList<CatalogItem>() { TotalCount = items.Count, Data = items };
    }

    public async Task<int?> RemoveAsync(int id)
    {
        var item = await _dbContext.CatalogItems
            .FirstOrDefaultAsync(f => f.Id == id);

        var result = _dbContext.Remove(item!);
        await _dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<int?> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.CatalogItems
            .FirstOrDefaultAsync(f => f.Id == id);
        if (item is not null)
        {
            item.CatalogBrandId = catalogBrandId;
            item.CatalogTypeId = catalogTypeId;
            item.Description = description;
            item.Name = name;
            item.PictureFileName = pictureFileName;
            item.Price = price;
            item.AvailableStock = availableStock;

            item = _dbContext.Update(item).Entity;
            await _dbContext.SaveChangesAsync();
        }

        return item!.Id;
    }
}