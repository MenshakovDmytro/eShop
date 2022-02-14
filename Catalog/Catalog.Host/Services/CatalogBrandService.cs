﻿using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        public Task<int?> AddAsync(string name)
        {
            return ExecuteSafeAsync(() => _catalogBrandRepository.AddAsync(name));
        }

        public async Task<ItemsListResponse<CatalogBrandDto>> GetBrandsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogBrandRepository.GetBrandsAsync();
                return new ItemsListResponse<CatalogBrandDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList(),
                };
            });
        }

        public Task<bool> RemoveAsync(string name)
        {
            return ExecuteSafeAsync(() => _catalogBrandRepository.RemoveAsync(name));
        }

        public Task<bool> UpdateAsync(string oldName, string newName)
        {
            return ExecuteSafeAsync(() => _catalogBrandRepository.UpdateAsync(oldName, newName));
        }
    }
}
