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
        [Required(ErrorMessage = "Wprowadź kategorię produktu!")]
        [Display(Name = "Kategoria produktu")]

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwę produktu!")]
        [MaxLength(50, ErrorMessage = "Nazwa może mieć maksymalnie 50 znaków")]
        [Display(Name = "Nazwa produktu")]

        public string Name { get; set; }
     
        [Required]
        public DateTime AddData { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany!")]
        [MaxLength(50, ErrorMessage = "Opis może mieć maksymalnie 5000 znaków")]
        [Display(Name = "Opis produktu")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Cena jest wymagany!")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        public int Bestseller { get; set; } = 0; 
        public bool Hidden { get; set; }
        [Required(ErrorMessage = "Krótki opis jest wymagany!")]
        [MaxLength(50, ErrorMessage = "Opis może mieć maksymalnie 200 znaków")]
        [Display(Name = "Krótki opis produktu")]
        public string ShortDescription { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Zdjęcie produktu")]
        public IFormFile Image { get; set; }

        public virtual ApplicationUser User { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ICollection<Category> Category { get; set; }
    }
}
