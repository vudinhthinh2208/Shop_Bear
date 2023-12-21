using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop_Bear.Models.EF
{
    [Table("tb_Order")]
    public class Order : CommonAbstract
    {
        public Order() 
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Code {  get; set; }
        public string? CustomerName {  get; set; }
        public string? Phone {  get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public decimal? TotalAmount {  get; set; }
        public int Quantity {  get; set; }
        public int TypePayment { get; set; }
        public ICollection<OrderDetail> OrderDetails { get;}
    }
}
