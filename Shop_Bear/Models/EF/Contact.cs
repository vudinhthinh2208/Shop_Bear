using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Bear.Models.EF
{
    [Table("tb_Contact")]
    public class Contact : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        [StringLength(150, ErrorMessage ="Không nhập quá 150 ký tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Nhập đúng định dạng Email")]
        public string Email { get; set; }
        public string? Website { get; set; }
        public string? Message { get; set; }
        public bool IsRead {  get; set; }
    }
}
