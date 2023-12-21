using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Bear.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category() 
        {
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nhập tên danh mục")]
        [StringLength(150)]
        public string Title {  get; set; }
        public string? Alias { get; set; }
        public string? Link {  get; set; }
        [Required(ErrorMessage = "Nhập mô tả")]
        public string? Description { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set;}
        public string? SeoKeywords {  get; set; }
        public bool IsActive { get; set; }
        public int? Position {  get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Posts> Posts { get; set; }

    }
}
