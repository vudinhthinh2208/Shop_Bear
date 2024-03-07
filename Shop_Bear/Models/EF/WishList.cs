using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Bear.Models.EF
{
	[Table("tb_WishList")]
	public class WishList
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("Product")]
		public int ProductId {  get; set; }
		public string UserName {  get; set; }
		public DateTime CreatedDate { get; set; }
		public virtual Product Product { get; set; }
	}
}
