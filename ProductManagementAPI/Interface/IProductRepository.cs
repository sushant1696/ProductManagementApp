using ProductManagementAPI.Model;

namespace ProductManagementAPI.Interface
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllAsync();
		Task<Product> GetByIdAsync(int id);
		Task <Product> AddAsync(ProductRequest product);
		Task<Product> UpdateAsync(Product product);
		Task<bool> DeleteAsync(int id);
		Task<bool> DecrementStockAsync(int id, int quantity);
		Task<bool> AddToStockAsync(int id, int quantity);
	}
	
}
