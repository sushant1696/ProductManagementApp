﻿using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.DBContext;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Model;
using ProductManagementAPI.SKUGenerateFactory;

namespace ProductManagementAPI.Repository
{
	public class ProductsRepository
	{
	}
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _context;
		private readonly Utility _utility;
		private readonly ILogger<ProductRepository> _logger;

		public ProductRepository(AppDbContext context, Utility utility, ILogger<ProductRepository> logger)
		{
			_context = context;
			_utility = utility;
			_logger = logger;
		}

		public async Task<Product> AddAsync(ProductRequest product)
		{
			Product? response = null;
			int result = 0;
			try
			{
				var skuGenerator = SkuGeneratorFactory.GetSkuGenerator(product.Brand);
				string Sku = skuGenerator.GenerateSku(product);
				int productNo = await _utility.GenerateProductIdAsync();
				if (productNo <= 999999)
				{
					Product request = new Product()
					{
						Price = product.Price,
						ProductId = productNo,
						Sku = Sku,
						NoOfStock = product.NoOfStock,
						Description = product.Description,
						ProductName = product.ProductName,
						ProductType = product.ProductType,
						Discount = product.Discount,
						Color = product.Color,
						Brand = product.Brand,
						Size = product.Size,
						Weight = product.Weight,
						IsActive = product.IsActive,
						CreatedAt = product.CreatedAt,
						UpdatedAt = product.UpdatedAt,
					};
					await _context.tbl_products.AddAsync(request);
					result = await _context.SaveChangesAsync();
					if (result > 0)
					{
						return response = request;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in AddAsync");
				return response;
			}
			return response;
		}
		public async Task<List<Product>> GetAllAsync()
		{
			return await _context.tbl_products.ToListAsync();
		}
		public async Task<Product> GetByIdAsync(int ProductId)
		{
			Product response = null;
			try
			{

				return await _context.tbl_products.SingleOrDefaultAsync(p => p.ProductId == ProductId); ;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in GetByIdAsync");
				return response;
			}

		}
		public async Task<Product> UpdateAsync(Product product)
		{
			Product existingProduct = new();
			try
			{
				existingProduct = await _context.tbl_products.SingleOrDefaultAsync(p => p.ProductId == product.ProductId);
				if (existingProduct == null) return null;

				existingProduct.ProductId = product.ProductId;
				existingProduct.NoOfStock = product.NoOfStock;
				existingProduct.ProductName = product.ProductName;
				existingProduct.Price = product.Price;
				existingProduct.Description = product.Description;
				existingProduct.Color = product.Color;
				existingProduct.Size = product.Size;
				existingProduct.Discount = product.Discount;

				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UpdateAsync");
				return null;
			}
			return existingProduct;
		}
		public async Task<bool> DeleteAsync(int ProductId)
		{
			try
			{
				//var product = await _context.tbl_products.FindAsync(id);
				var product = await _context.tbl_products.SingleOrDefaultAsync(p => p.ProductId == ProductId);
				if (product == null) return false;

				_context.tbl_products.Remove(product);
				int result = await _context.SaveChangesAsync();
				if (result <= 0)
				{
					return false;

				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete product {ProductId}");
				return false;
			}

		}
		public async Task<bool> DecrementStockAsync(int ProductId, int quantity)
		{
			try
			{
				var product = await _context.tbl_products.SingleOrDefaultAsync(p => p.ProductId == ProductId);
				if (product == null || product.NoOfStock < quantity) return false;

				product.NoOfStock -= quantity;
				int result = await _context.SaveChangesAsync();
				if (result <= 0)
				{
					return false;

				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				return true;
			}


		}
		public async Task<bool> AddToStockAsync(int ProductId, int quantity)
		{
			try
			{
				var product = await _context.tbl_products.SingleOrDefaultAsync(p => p.ProductId == ProductId);
				if (product == null) return false;

				product.NoOfStock += quantity;
				int result = await _context.SaveChangesAsync();
				if (result <= 0)
				{
					return false;

				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}

}
