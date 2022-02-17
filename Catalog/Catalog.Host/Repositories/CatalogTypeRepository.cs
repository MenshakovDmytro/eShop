using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name)
        {
            var brand = await _dbContext.AddAsync(new CatalogType()
            {
                Type = name
            });

            await _dbContext.SaveChangesAsync();

            return brand.Entity.Id;
        }

        public async Task<ItemsList<CatalogType>> GetTypesAsync()
        {
            var types = await _dbContext.CatalogTypes
                .ToListAsync();

            return new ItemsList<CatalogType>() { TotalCount = types.Count, Data = types };
        }

        public async Task<int?> RemoveAsync(int id)
        {
            var type = await _dbContext.CatalogTypes
            .FirstOrDefaultAsync(f => f.Id == id);

            var result = _dbContext.Remove(type!);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<int?> UpdateAsync(int id, string name)
        {
            var type = await _dbContext.CatalogTypes
            .FirstOrDefaultAsync(f => f.Id == id);

            if (type is not null)
            {
                type.Type = name;

                type = _dbContext.Update(type).Entity;
                await _dbContext.SaveChangesAsync();
            }

            return type!.Id;
        }
    }
}
