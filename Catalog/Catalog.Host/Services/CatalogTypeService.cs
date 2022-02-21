using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public async Task<ItemsListResponse<CatalogTypeDto>> GetTypesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogTypeRepository.GetTypesAsync();
                return new ItemsListResponse<CatalogTypeDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList(),
                };
            });
        }
    }
}
