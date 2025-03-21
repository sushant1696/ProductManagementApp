using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Model;

namespace ProductManagementAPI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly ILogger<ProductController> _logger;
		public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

		[HttpPost("CreateProduct")]
		public async Task<CommonResponse<Product>> CreateProduct([FromBody] ProductRequest product)
		{
			CommonResponse<Product> result = new();
			if (!ModelState.IsValid)
				return result.Failed(ErrorCode.BadRequest, "InValid Request", "Please Enter Valida Data");
			try
			{
				_logger.LogInformation("CreateProduct Request Body {@product}", product);
				Product response = await _productRepository.AddAsync(product);
				if (response is not null)
				{
					return result.Ok(response);
				}
				else
				{
					return result.Failure(response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exceprion in CreateProduct");

				return result.Exception(ex);
			}
		}
		[HttpGet("GetAllProducts")]
		public async Task<CommonResponse<List<Product>>> GetAllProducts()
		{
			CommonResponse<List<Product>> result = new();
			List<Product> listResult = new();
			try
			{
				
				listResult = await _productRepository.GetAllAsync();
				if (listResult is not null && listResult.Count > 0)
				{
					return result.Ok(listResult);
				}
				else
				{
					return result.NotFound();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exceprion in CreateProduct");
				return result.Exception(ex);

			}

		}
		[HttpGet("GetById/{id}")]
		public async Task<CommonResponse<Product>> GetById(int id)
		{
			CommonResponse<Product> commonResponse = new();
			Product product = new();
			try
			{
				_logger.LogInformation(" Requested id {@id}", id);
				if (id <= 0)
				{
					return commonResponse.Failed(ErrorCode.BadRequest, "Invalid Request", "Id should be greater than 0");
				}
				product = await _productRepository.GetByIdAsync(id);
				if (product == null)
				{
					return commonResponse.Failure(product);
				}
				else
				{
					return commonResponse.Ok(product);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exception in GetById");
				return commonResponse.Exception(ex);
			}

		}

		// PUT: api/Product/{id}
		[HttpPut("UpdateProduct/{id}")]
		public async Task<CommonResponse<Product>> UpdateProduct(int id, [FromBody] Product product)
		{
			CommonResponse<Product> commonResponse = new();
			Product updatedProduct = new();
			try
			{
				_logger.LogInformation("UpdateProduct Request Body {@product}", product);
				if (id != product.ProductId)
				{
					return commonResponse.Failed(ErrorCode.BadRequest, "InValid Request");
				}

				updatedProduct = await _productRepository.UpdateAsync(product);
				if (updatedProduct == null)
				{
					return commonResponse.Failure(updatedProduct);
				}
				else
				{
					return commonResponse.Ok(updatedProduct);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "error in UpdateProduct");
				return commonResponse.Exception(ex);
			}
		}
		// DELETE: api/Product/{id}
		[HttpDelete("DeleteProduct{id}")]
		public async Task<CommonResponse<string>> DeleteProduct(int id)
		{
			CommonResponse<string> commonResponse = new();
			try
			{
				_logger.LogInformation("DeleteProduct Request Id {@id}", id);
				if (id <= 0)
				{
					return commonResponse.Failed(ErrorCode.BadRequest, "InValid Request", "");
				}
				var deleted = await _productRepository.DeleteAsync(id);
				if (!deleted)
				{
					return commonResponse.Failure("Delete Failure");
				}
				else
				{
					return commonResponse.Ok("Delete Success");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error In deleteProduct");
				return commonResponse.Exception(ex);
			}
		}
		// PUT: api/products/decrement-stock/{id}/{quantity}
		[HttpPut("decrement-stock/{id}/{quantity}")]
		public async Task<IActionResult> DecrementStock(int id, int quantity)
		{
			try
			{
				_logger.LogInformation("Decrement Request id {@id}", id);
				var success = await _productRepository.DecrementStockAsync(id, quantity);
				if (!success) return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex,"Error in DecrementStock");
				return NotFound();
			}
			return Ok();
		}

		// PUT: api/products/add-to-stock/{id}/{quantity}
		[HttpPut("add-to-stock/{id}/{quantity}")]
		public async Task<IActionResult> AddToStock(int id, int quantity)
		{
			try
			{
				_logger.LogInformation("Add Stock Request id {@id}", id);
				var success = await _productRepository.AddToStockAsync(id, quantity);
				if (!success) return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in AddToStock");
			}
			return Ok();
		}
	}
}
