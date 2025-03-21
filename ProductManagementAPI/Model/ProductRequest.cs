using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Model
{
	
	public class ProductRequest
	{
		
		public int NoOfStock { get; set; }
		
		[Required]
		[StringLength(100)]
		public string ProductName { get; set; } = string.Empty;
		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; }
		public string Description { get; set; } = string.Empty;
		[Required]
		[StringLength(100)]
		public string ProductType { get; set; } = string.Empty;
		[Required]
		[StringLength(100)]
		public string Brand { get; set; } = string.Empty;

		[Column(TypeName = "decimal(10,2)")]
		public decimal? Discount { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		public decimal? Weight { get; set; }

		[StringLength(100)]
		public string? Color { get; set; }

		[StringLength(100)]
		public string? Size { get; set; }

		public bool IsActive { get; set; } = true;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime? UpdatedAt { get; set; }

	}
}
