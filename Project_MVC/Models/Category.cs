using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwę kategorii")]
        [StringLength(100)]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; } = null;
        public int? ChildCategoryId { get; set; } = null;
        public virtual ICollection<Product> Products { get; set; }

    }
    

}
