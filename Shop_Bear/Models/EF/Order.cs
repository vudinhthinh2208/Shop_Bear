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
		[Required(ErrorMessage = "Nhập tên người nhận")]
		public string? CustomerName {  get; set; }

		[Required(ErrorMessage = "Nhập số điện thoại người nhận")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ.")]
		[StringLength(11, MinimumLength = 9, ErrorMessage = "Số điện thoại phải có từ 9 đến 11 chữ số.")]
		public string? Phone { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
        public string? Address { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
        public decimal? TotalAmount {  get; set; }
        public int Quantity {  get; set; }
        public int TypePayment { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate {  get; set; }
        public DateTime DatePayment {  get; set; }
        public ICollection<OrderDetail> OrderDetails { get;}
    }
}
