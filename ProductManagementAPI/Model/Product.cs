using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Model
{
	//public class Product
	//{
	//[Key]
	//[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // This ensures auto-incrementing
	//public int Id { get; set; }
	//public int ProductId { get; set; }
	//public int NoOfStock { get; set; }
	//public string Sku { get; set; } = string.Empty;
	//public string ProductName { get; set; } = string.Empty;
	//public decimal Price { get; set; }
	//public string Description { get; set; } = string.Empty;
	//public string ProductType { get; set; } = string.Empty;
	[Table("tbl_products")]
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }  // Primary Key, Auto-Incremented

		public int ProductId { get; set; }  // This is a regular property, NOT a key

		public int NoOfStock { get; set; }

		[Required]
		public string Sku { get; set; }

		[Required]
		public string ProductName { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		public string Description { get; set; }

		public string ProductType { get; set; }

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

