using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.DBContext;

namespace ProductManagementAPI.Repository
{
	public class Utility
	{
		private readonly AppDbContext _context;

		public Utility(AppDbContext context)
		{
			_context = context;
		}

		//public async Task<int> GenerateProductIdAsync()
		//{
		//	int nextProductId = 100000; // Default starting point if no records exist

		//	using (var transaction = await _context.Database.BeginTransactionAsync())
		//	{
		//		try
		//		{
		//			// Lock the table to prevent concurrent reads
		//			var result = await _context.tbl_products
		//				.FromSqlRaw("SELECT id FROM tbl_products ORDER BY id DESC LIMIT 1 FOR UPDATE")
		//				.FirstOrDefaultAsync();

		//			if (result != null)
		//			{
		//				nextProductId = result.ProductId + 1;
		//			}

		//			// Commit the transaction to release the lock
		//			await transaction.CommitAsync();
		//		}
		//		catch (Exception ex)
		//		{
		//			// Rollback transaction in case of error
		//			await transaction.RollbackAsync();
		//			Console.Error.WriteLine($"Error occurred during ProductId generation: {ex.Message}");
		//			throw;
		//		}
		//	}

		//	return nextProductId;
		//}
		public async Task<int> GenerateProductIdAsync()
		{
			int nextProductId = 100000; // Default starting point if no records exist

			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					// Lock the table to prevent concurrent reads
					var lastProduct = await _context.tbl_products
						.OrderByDescending(p => p.ProductId)
						.FirstOrDefaultAsync();

					if (lastProduct != null)
					{
						if (lastProduct.Id<=999999)
						{
							nextProductId = lastProduct.ProductId + 1;
						}
						
					}

					// Commit the transaction to release the lock
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					// Rollback transaction in case of error
					await transaction.RollbackAsync();
					Console.Error.WriteLine($"Error occurred during ProductId generation: {ex.Message}");
					throw;
				}
			}

			return nextProductId;
		}

	}
}
