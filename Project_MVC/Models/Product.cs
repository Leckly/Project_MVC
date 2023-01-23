using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Models
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwę produktu!")]
        [MaxLength(50, ErrorMessage = "Nazwa może mieć maksymalnie 50 znaków")]
        [Display(Name = "Nazwa produktu")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwę autora")]
        [StringLength(100)]

       
        public string Author { get; set; }
        [Required]
        public DateTime AddData { get; set; }

        [Required(ErrorMessage = "Opis przepisu jest wymagany!")]
        [MaxLength(50, ErrorMessage = "Opis może mieć maksymalnie 5000 znaków")]
        [Display(Name = "Opis przepisu")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Bestseller { get; set; }
        public bool Hidden { get; set; }
        public string ShortDescription { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Zdjęcie produktu")]
        public IFormFile Image { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}
