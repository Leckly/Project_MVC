using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string UserId { get; set; }


        [Required(ErrorMessage = "Wprowadz imię")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Wprowadz nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Wprowadz adres")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Wprowadz miasto")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Wprowadz kod pocztowy")]
        [StringLength(6)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić numer telefonu")]
        [StringLength(20)]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Wprowadź swój adres e-mail.")]
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }

        public string Comment { get; set; }

        public DateTime DateAdded { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal OrderValue { get; set; }

        public List<OrderItem> OrderItem { get; set; }
    }


    public enum OrderStatus
    {
        Nowe,
        Zrealizowane
    }
}
    

