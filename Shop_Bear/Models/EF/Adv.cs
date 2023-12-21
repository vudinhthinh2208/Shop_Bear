﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Bear.Models.EF
{
    [Table("tb_Adv")]
    public class Adv : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title {  get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Image { get; set; }
        [StringLength(500)]
        public string Link { get; set; }
        public int Type {  get; set; }
    }
}
