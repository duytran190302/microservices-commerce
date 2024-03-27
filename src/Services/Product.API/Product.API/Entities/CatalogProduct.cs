using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace Product.API.Entities
{
	public class CatalogProduct: EntityAuditBase<int>
	{
		[Required]             
		[Column("No", TypeName = "varchar(50)")]
		public string No { get; set; }
		[Required]
		[Column("Name", TypeName = "nvarchar(250)")]
		public string Name { get; set; }
		[Column("Description", TypeName = "text")]
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
