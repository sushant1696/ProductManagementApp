using ProductManagementAPI.Interface;
using ProductManagementAPI.SKUGenerate;

namespace ProductManagementAPI.SKUGenerateFactory
{
	public class Factory
	{
	}
	// SkuGeneratorFactory.cs
	public static class SkuGeneratorFactory
	{
		public static ISkuGenerator GetSkuGenerator(string brand)
		{
			return brand.ToLower() switch
			{
				"nike" => new NikeSkuGenerator(),
				"adidas" => new AdidasSkuGenerator(),
				_ => new DefaultSkuGenerator(),
			};
		}
	}

}
