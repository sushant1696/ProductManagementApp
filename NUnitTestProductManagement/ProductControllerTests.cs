using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductManagementAPI.Controllers;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Model;

namespace NUnitTestProductManagement
{
	[TestFixture]
	public class ProductControllerTests
	{
		private Mock<IProductRepository> _mockRepo;
		private Mock<ILogger<ProductController>> _mockLogger;
		private ProductController _controller;

		[OneTimeSetUp]
		public void GlobalSetup()
		{
			_mockRepo = new Mock<IProductRepository>();
			_mockLogger = new Mock<ILogger<ProductController>>();
		}

		[SetUp]
		public void TestSetup()
		{
			_controller = new ProductController(_mockRepo.Object, _mockLogger.Object);
		}

		[TearDown]
		public void TearDown()
		{
			if (_controller is IDisposable disposableController)
			{
				disposableController.Dispose();
			}

			_controller = null;  // Ensure cleanup
		}

		[OneTimeTearDown]
		public void GlobalTearDown()
		{
			_mockRepo = null;
			_mockLogger = null;
		}

		[Test]
		public async Task CreateProduct_ValidRequest_ReturnsOkResponse()
		{
			var productRequest = new ProductRequest { ProductName = "Test Product", ProductType = "Food" };
			var product = new Product { ProductId = 1, ProductName = "Test Product", Sku = "SKU123" };
			_mockRepo.Setup(x => x.AddAsync(productRequest)).ReturnsAsync(product);

			var result = await _controller.CreateProduct(productRequest);

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(product, result.Data);
		}

		[Test]
		public async Task CreateProduct_InvalidModel_ReturnsFailedResponse()
		{
			_controller.ModelState.AddModelError("ProductName", "Required");
			var productRequest = new ProductRequest();

			var result = await _controller.CreateProduct(productRequest);

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual("InValid Request", result.Message);
		}

		[Test]
		public async Task GetAllProducts_ReturnsOkResponse()
		{
			var products = new List<Product> { new Product { ProductId = 1, ProductName = "Test Product", Sku = "SKU123" } };
			_mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

			var result = await _controller.GetAllProducts();

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(products, result.Data);
		}

		[Test]
		public async Task GetById_InvalidId_ReturnsFailedResponse()
		{
			var result = await _controller.GetById(0);

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual("Invalid Request", result.Message);
		}

		[Test]
		public async Task UpdateProduct_ValidRequest_ReturnsOkResponse()
		{
			var product = new Product { ProductId = 1, ProductName = "Updated Product", Sku = "SKU123" };
			_mockRepo.Setup(x => x.UpdateAsync(product)).ReturnsAsync(product);

			var result = await _controller.UpdateProduct(1, product);

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(product, result.Data);
		}

		[Test]
		public async Task DeleteProduct_ValidId_ReturnsOkResponse()
		{
			_mockRepo.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);

			var result = await _controller.DeleteProduct(1);

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("Delete Success", result.Data);
		}

		[Test]
		public async Task DecrementStock_ValidRequest_ReturnsOkResult()
		{
			_mockRepo.Setup(x => x.DecrementStockAsync(1, 5)).ReturnsAsync(true);

			var result = await _controller.DecrementStock(1, 5) as OkResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
		}

		[Test]
		public async Task AddToStock_ValidRequest_ReturnsOkResult()
		{
			_mockRepo.Setup(x => x.AddToStockAsync(1, 5)).ReturnsAsync(true);

			var result = await _controller.AddToStock(1, 5) as OkResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
		}

		[Test]
		public async Task AddToStock_InvalidId_ReturnsNotFound()
		{
			_mockRepo.Setup(x => x.AddToStockAsync(0, 5)).ReturnsAsync(false);

			var result = await _controller.AddToStock(0, 5);

			Assert.IsInstanceOf<NotFoundResult>(result);
		}
	}
}