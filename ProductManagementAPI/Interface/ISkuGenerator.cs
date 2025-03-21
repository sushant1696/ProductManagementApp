using ProductManagementAPI.Model;

namespace ProductManagementAPI.Interface
{
	public interface ISkuGenerator
	{
		string GenerateSku(ProductRequest product);
	}
}
