using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Utilities.Statics;

namespace POS.Test.Category;

[TestClass]
public class CategoryApplicationTest
{
    // keep existing factory initialization for integration scenarios if needed
    private static WebApplicationFactory<Program>? _factory = null;
    private static IServiceScopeFactory? _scopeFactory = null;

    private CategoryApplication? _service;
    //private FakeCategoryRepository? _repo;

    [ClassInitialize]
    public static void Initialize(TestContext _testContext)
    {
        _factory ??= new CustomWebApplicationFactory();
        _scopeFactory ??= _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    //[TestInitialize]
    //public void Setup()
    //{
    //    // Prepare a simple in-memory repository and mapper for unit tests
    //    _repo = new FakeCategoryRepository();
    //    var uow = new FakeUnitOfWork(_repo);

    //    var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CategoryMappingsProfile()));
    //    var mapper = mapperConfig.CreateMapper();

    //    var validator = new CategoryValidator();

    //    _service = new CategoryApplication(uow, mapper, validator);
    //}

    //[TestMethod]
    //public async Task ListCategories_ShouldReturnItems_WhenRepositoryHasData()
    //{
    //    // Arrange
    //    var category = new Category { Id = 1, Name = "Cat 1", Description = "Desc" };
    //    _repo!.SetItems(new[] { category });

    //    // Act
    //    var result = await _service!.ListCategories(new BaseFiltersRequest());

    //    // Assert
    //    Assert.IsTrue(result.IsSuccess);
    //    Assert.IsNotNull(result.Data);
    //    Assert.AreEqual(1, result.Data!.Items!.Count);
    //    Assert.AreEqual(category.Id, result.Data.Items[0].CategoryId);
    //}

    //[TestMethod]
    //public async Task ListSelectCategories_ShouldReturnSelectDtos()
    //{
    //    // Arrange
    //    var category = new Category { Id = 2, Name = "Cat Select" };
    //    _repo!.SetItems(new[] { category });

    //    // Act
    //    var result = await _service!.ListSelectCategories();

    //    // Assert
    //    Assert.IsTrue(result.IsSuccess);
    //    Assert.IsNotNull(result.Data);
    //    var list = result.Data!.ToList();
    //    Assert.AreEqual(1, list.Count);
    //    Assert.AreEqual(category.Id, list[0].CategoryId);
    //}

    //[TestMethod]
    //public async Task CategoryById_ShouldReturnNotFound_WhenMissing()
    //{
    //    // Arrange - ensure repository is empty
    //    _repo!.SetItems(Array.Empty<Category>());

    //    // Act
    //    var result = await _service!.CategoryById(999);

    //    // Assert
    //    Assert.IsFalse(result.IsSuccess);
    //    StringAssert.Contains(result.Message ?? string.Empty, "No se encontró la categoría");
    //}

    //[TestMethod]
    //public async Task CategoryById_ShouldReturnDto_WhenFound()
    //{
    //    // Arrange
    //    var category = new Category { Id = 5, Name = "Found" };
    //    _repo!.SetItems(new[] { category });

    //    // Act
    //    var result = await _service!.CategoryById(5);

    //    // Assert
    //    Assert.IsTrue(result.IsSuccess);
    //    Assert.IsNotNull(result.Data);
    //    Assert.AreEqual(5, result.Data!.CategoryId);
    //}

    //[TestMethod]
    //public async Task RegisterCategory_WhenSendingNullValuesOrEmpty_ShouldReturnValidationErrors()
    //{
    //    using var scope = _scopeFactory!.CreateScope();
    //    var context = scope.ServiceProvider.GetService<ICategoryApplication>();
    //    // Arrange
    //    var request = new CategoryRequestDto { Name = null, Description = null, State = 2 };
    //    // Act
    //    var result = await context!.RegisterCategory(request);
    //    // Assert
    //    //Assert.IsFalse(result.IsSuccess);
    //    //Assert.IsNotNull(result.Errors);
    //    //Assert.IsTrue(result.Errors!.Any());
    //    Assert.AreEqual(ReplyMessage.MESSAGE_FAILED, result.Message);
    //}


    [TestMethod]
    public async Task RegisterCategory_ShouldReturnSuccess_WhenRepositoryRegisters()
    {
        using var scope = _scopeFactory!.CreateScope();
        var context = scope.ServiceProvider.GetService<ICategoryApplication>();

        // Arrange
        var request = new CategoryRequestDto { Name = "New", Description = "Desc", State = 1 };

        // Act
        var result = await context!.RegisterCategory(request);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Data);
        Assert.AreEqual(ReplyMessage.MESSAGE_SAVE, result.Message);
    }

    [TestMethod]
    public async Task EditCategory_ShouldReturnSuccess_WhenRepositoryEdits()
    {
        using var scope = _scopeFactory!.CreateScope();
        var context = scope.ServiceProvider.GetService<ICategoryApplication>();

        // Arrange
        var request = new CategoryRequestDto { Name = "Updated", Description = "D", State = 1 };

        // Act
        var result = await context!.EditCategory(10, request);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsTrue(result.Data);
        Assert.AreEqual(ReplyMessage.MESSAGE_UPDATE, result.Message);
    }

    //[TestMethod]
    //public async Task RemoveCategory_ShouldReturnNotFound_WhenMissing()
    //{
    //    // Arrange
    //    _repo!.SetItems(Array.Empty<Category>());

    //    // Act
    //    var result = await _service!.RemoveCategory(999);

    //    // Assert
    //    Assert.IsFalse(result.IsSuccess);
    //    Assert.AreEqual(ReplyMessage.MESSAGE_QUERY_EMPTY, result.Message);
    //}

    //[TestMethod]
    //public async Task RemoveCategory_ShouldReturnSuccess_WhenRemoved()
    //{
    //    // Arrange
    //    var existing = new Category { Id = 11, Name = "ToRemove" };
    //    _repo!.SetItems(new[] { existing });

    //    // Act
    //    var result = await _service!.RemoveCategory(11);

    //    // Assert
    //    Assert.IsTrue(result.IsSuccess);
    //    Assert.IsTrue(result.Data);
    //    Assert.AreEqual(ReplyMessage.MESSAGE_DELETE, result.Message);
    //}

    #region Test helpers (fakes)
    //internal class FakeUnitOfWork : IUnitOfWork
    //{
    //    public ICategoryRepository Category { get; }

    //    public FakeUnitOfWork(ICategoryRepository category)
    //    {
    //        Category = category;
    //    }

    //    public void Dispose() { }
    //    public void SaveChanges() { }
    //    public Task SaveChangesAsync() => Task.CompletedTask;
    //}

    //internal class FakeCategoryRepository : ICategoryRepository
    //{
    //    private readonly List<Domain.Entities.Category> _items = new();
    //    private BaseEntityResponse<Domain.Entities.Category>? _listResponse = null;

    //    public void SetItems(IEnumerable<Domain.Entities.Category> items)
    //    {
    //        _items.Clear();
    //        _items.AddRange(items);
    //        _listResponse = new BaseEntityResponse<Domain.Entities.Category> { TotalRecords = _items.Count, Items = _items.ToList() };
    //    }

    //    public Task<BaseEntityResponse<Domain.Entities.Category>> ListCategories(BaseFiltersRequest filters)
    //    {
    //        return Task.FromResult(_listResponse!);
    //    }

    //    public Task<IEnumerable<Domain.Entities.Category>> GetAllAsync()
    //    {
    //        return Task.FromResult<IEnumerable<Domain.Entities.Category>>(_items.ToList());
    //    }

    //    public Task<Domain.Entities.Category> GetByIdAsync(int id)
    //    {
    //        return Task.FromResult(_items.FirstOrDefault(x => x.Id == id)!);
    //    }

    //    public Task<bool> RegisterAsync(Domain.Entities.Category entity)
    //    {
    //        entity.Id = (_items.Any() ? _items.Max(x => x.Id) + 1 : 1);
    //        _items.Add(entity);
    //        return Task.FromResult(true);
    //    }

    //    public Task<bool> EditAsync(Domain.Entities.Category entity)
    //    {
    //        var found = _items.FirstOrDefault(x => x.Id == entity.Id);
    //        if (found is null) return Task.FromResult(false);
    //        found.Name = entity.Name;
    //        found.Description = entity.Description;
    //        found.State = entity.State;
    //        return Task.FromResult(true);
    //    }

    //    public Task<bool> RemoveAsync(int id)
    //    {
    //        var found = _items.FirstOrDefault(x => x.Id == id);
    //        if (found is null) return Task.FromResult(false);
    //        _items.Remove(found);
    //        return Task.FromResult(true);
    //    }

    //    // Not used by tests but required by interface
    //    public IQueryable<Domain.Entities.Category> GetEntityQueryable(System.Linq.Expressions.Expression<Func<Domain.Entities.Category, bool>>? filter = null)
    //    {
    //        return _items.AsQueryable();
    //    }

    //    public IQueryable<TDTO> Ordering<TDTO>(POS.Infrastructure.Commons.Bases.Request.BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
    //    {
    //        return queryable;
    //    }
    //}
    #endregion
}


