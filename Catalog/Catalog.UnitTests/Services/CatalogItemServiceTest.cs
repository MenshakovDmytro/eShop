using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogItemService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogItem _testCatalogItem = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        PictureFileName = "1.png",
        CatalogBrandId = 1,
        CatalogTypeId = 1
    };

    private readonly CatalogItemDto _testCatalogItemDto = new CatalogItemDto()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        PictureUrl = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogItemService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testCatalogItem.Name, _testCatalogItem.Description, _testCatalogItem.Price, _testCatalogItem.AvailableStock, _testCatalogItem.CatalogBrandId, _testCatalogItem.CatalogTypeId, _testCatalogItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testCatalogItem.Name, _testCatalogItem.Description, _testCatalogItem.Price, _testCatalogItem.AvailableStock, _testCatalogItem.CatalogBrandId, _testCatalogItem.CatalogTypeId, _testCatalogItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.RemoveAsync(testResult))
        .ReturnsAsync(testResult);

        // act
        var result = await _catalogService.RemoveAsync(testResult);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Failed()
    {
        // arrange
        var testId = -1;
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.RemoveAsync(testId))
        .ReturnsAsync(testResult);

        // act
        var result = await _catalogService.RemoveAsync(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.UpdateAsync(
            testResult,
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateAsync(testResult, _testCatalogItem.Name, _testCatalogItem.Description, _testCatalogItem.Price, _testCatalogItem.AvailableStock, _testCatalogItem.CatalogBrandId, _testCatalogItem.CatalogTypeId, _testCatalogItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        var testId = -1;
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.UpdateAsync(
            testId,
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateAsync(testId, _testCatalogItem.Name, _testCatalogItem.Description, _testCatalogItem.Price, _testCatalogItem.AvailableStock, _testCatalogItem.CatalogBrandId, _testCatalogItem.CatalogTypeId, _testCatalogItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetByBrandAsync_Success()
    {
        // arrange
        var testBrand = "TestBrand";
        var testTotalCount = 1;
        var testResult = new ItemsList<CatalogItem>()
        {
            TotalCount = testTotalCount,
            Data = new List<CatalogItem>() { _testCatalogItem }
        };

        _catalogItemRepository.Setup(s => s.GetByBrandAsync(
            It.Is<string>(i => i.Equals(testBrand))))
            .ReturnsAsync(testResult);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(_testCatalogItem))))
            .Returns(_testCatalogItemDto);

        // act
        var result = await _catalogService.GetByBrandAsync(testBrand);

        // assert
        result.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.Data.Should().AllBeEquivalentTo(_testCatalogItemDto);
    }

    [Fact]
    public async Task GetByBrandAsync_Failed()
    {
        // arrange
        var testBrand = string.Empty;

        _catalogItemRepository.Setup(s => s.GetByBrandAsync(
            It.Is<string>(i => i.Equals(testBrand))))
            .ReturnsAsync((ItemsList<CatalogItem>)null!);

        // act
        var result = await _catalogService.GetByBrandAsync(testBrand);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        var testId = 1;

        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i.Equals(testId))))
            .ReturnsAsync(_testCatalogItem);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(_testCatalogItem))))
            .Returns(_testCatalogItemDto);

        // act
        var result = await _catalogService.GetByIdAsync(testId);

        // assert
        result.Should().NotBeNull();
        result?.Should().BeEquivalentTo(_testCatalogItemDto);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        var testId = -1;

        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i.Equals(testId))))
            .ReturnsAsync((CatalogItem)null!);

        // act
        var result = await _catalogService.GetByIdAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByTypeAsync_Success()
    {
        // arrange
        var testType = "TestType";
        var testTotalCount = 1;
        var testResult = new ItemsList<CatalogItem>()
        {
            TotalCount = testTotalCount,
            Data = new List<CatalogItem>() { _testCatalogItem }
        };

        _catalogItemRepository.Setup(s => s.GetByTypeAsync(
            It.Is<string>(i => i.Equals(testType))))
            .ReturnsAsync(testResult);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(_testCatalogItem))))
            .Returns(_testCatalogItemDto);

        // act
        var result = await _catalogService.GetByTypeAsync(testType);

        // assert
        result.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.Data.Should().AllBeEquivalentTo(_testCatalogItemDto);
    }

    [Fact]
    public async Task GetByTypeAsync_Failed()
    {
        // arrange
        var testType = string.Empty;

        _catalogItemRepository.Setup(s => s.GetByTypeAsync(
            It.Is<string>(i => i.Equals(testType))))
            .ReturnsAsync((ItemsList<CatalogItem>)null!);

        // act
        var result = await _catalogService.GetByTypeAsync(testType);

        // assert
        result.Should().BeNull();
    }
}