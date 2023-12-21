﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Shop_Bear.Models.EF
{
    [Table("tb_News")]
    public class News : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [StringLength(100)]
        public string Title {  get; set; }

        public string? Alias {  get; set; }
        public string? Description { get; set; }
        public string? Detail {  get; set; }
        public string? Image {  get; set; }
        public int CategoryId {  get; set; }
        public string? SeoTitle {  get; set; }
        public string? SeoDescription { get; set;}
        public string? SeoKeywords {  get; set; }
        public bool IsActive {  get; set; }
        public virtual Category Category { get; set; }
    }
}