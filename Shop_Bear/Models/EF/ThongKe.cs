using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Bear.Models.EF
{
    [Table("ThongKes")]
    public class ThongKe
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime ThoiGian { get; set; }
		public long SoLuotTruyCap { get; set; }


	}
}
