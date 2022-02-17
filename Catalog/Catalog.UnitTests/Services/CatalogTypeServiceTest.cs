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
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogTypeService;

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogTypeService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogType _testCatalogType = new CatalogType()
        {
            Type = "TestType"
        };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogTypeService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.AddAsync(_testCatalogType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.AddAsync(_testCatalogType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Remove_Success()
        {
            // arrange
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.RemoveAsync(testResult))
            .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.RemoveAsync(testResult);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Remove_Failed()
        {
            // arrange
            var testId = -1;
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.RemoveAsync(testId))
            .ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.RemoveAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.UpdateAsync(
                testResult,
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.UpdateAsync(testResult, _testCatalogType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            var testId = -1;
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.UpdateAsync(
                testId,
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.UpdateAsync(testId, _testCatalogType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task GetTypes_Success()
        {
            // arrange
            var testTotalCount = 1;
            var testResult = new ItemsList<CatalogType>()
            {
                TotalCount = testTotalCount,
                Data = new List<CatalogType>() { _testCatalogType }
            };

            var testCatalogTypeDto = new CatalogTypeDto()
            {
                Type = "TestType"
            };

            _catalogTypeRepository.Setup(s => s.GetTypesAsync())
                .ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CatalogTypeDto>(
                It.Is<CatalogType>(i => i.Equals(_testCatalogType))))
                .Returns(testCatalogTypeDto);

            // act
            var result = await _catalogTypeService.GetTypesAsync();

            // assert
            result.Should().NotBeNull();
            result?.Count.Should().Be(testTotalCount);
            result?.Data.Should().AllBeEquivalentTo(testCatalogTypeDto);
        }

        [Fact]
        public async Task GetTypes_Failed()
        {
            // arrange
            _catalogTypeRepository.Setup(s => s.GetTypesAsync())
                .ReturnsAsync((ItemsList<CatalogType>)null!);

            // act
            var result = await _catalogTypeService.GetTypesAsync();

            // assert
            result.Should().BeNull();
        }
    }
}
