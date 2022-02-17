using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogBrandService;

        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogBrand _testCatalogBrand = new CatalogBrand()
        {
            Brand = "TestBrand"
        };

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.AddAsync(_testCatalogBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.AddAsync(_testCatalogBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Remove_Success()
        {
            // arrange
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.RemoveAsync(testResult))
            .ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.RemoveAsync(testResult);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Remove_Failed()
        {
            // arrange
            var testId = -1;
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.RemoveAsync(testId))
            .ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.RemoveAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                testResult,
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.UpdateAsync(testResult, _testCatalogBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var testId = -1;
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                testId,
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.UpdateAsync(testId, _testCatalogBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task GetBrands_Success()
        {
            // arrange
            var testTotalCount = 1;
            var testResult = new ItemsList<CatalogBrand>()
            {
                TotalCount = testTotalCount,
                Data = new List<CatalogBrand>() { _testCatalogBrand }
            };

            var testCatalogBrandDto = new CatalogBrandDto()
            {
                Brand = "TestBrand"
            };

            _catalogBrandRepository.Setup(s => s.GetBrandsAsync())
                .ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CatalogBrandDto>(
                It.Is<CatalogBrand>(i => i.Equals(_testCatalogBrand))))
                .Returns(testCatalogBrandDto);

            // act
            var result = await _catalogBrandService.GetBrandsAsync();

            // assert
            result.Should().NotBeNull();
            result?.Count.Should().Be(testTotalCount);
            result?.Data.Should().AllBeEquivalentTo(testCatalogBrandDto);
        }

        [Fact]
        public async Task GetBrands_Failed()
        {
            // arrange
            _catalogBrandRepository.Setup(s => s.GetBrandsAsync())
                .ReturnsAsync((ItemsList<CatalogBrand>)null!);

            // act
            var result = await _catalogBrandService.GetBrandsAsync();

            // assert
            result.Should().BeNull();
        }
    }
}
