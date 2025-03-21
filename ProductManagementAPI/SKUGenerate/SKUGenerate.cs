using ProductManagementAPI.Interface;
using ProductManagementAPI.Model;

namespace ProductManagementAPI.SKUGenerate
{
	
	// NikeSkuGenerator.cs
	public class NikeSkuGenerator : ISkuGenerator
	{
		public string GenerateSku(ProductRequest product)
		{
			return $"NIKE-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
		}
	}

	// AdidasSkuGenerator.cs
	public class AdidasSkuGenerator : ISkuGenerator
	{
		public string GenerateSku(ProductRequest product)
		{
			return $"ADI-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
		}
	}

	// DefaultSkuGenerator.cs (For other brands)
	public class DefaultSkuGenerator : ISkuGenerator
	{
		public string GenerateSku(ProductRequest product)
		{
			return $"{product.Brand.ToUpper()}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
		}
	}

}
