using HajurKoCarRental.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental.Models
{
    public class CarRent
    {

        [Key] public int CarRentId { get; set; }

        [ForeignKey("Id")] public int Id { get; set; }
        [ForeignKey("CarId")] public int CarId { get; set; }

        [Display(Name = "Date Rented")] public DateTime DateRented { get; set; }

        [Display(Name = "Date Return")] public DateTime DateReturn { get; set; }

        [Display(Name = "Rent price")]
        [Required(ErrorMessage = "Rent Price must be entered")]
        public int? RentPrice { get; set; }
        public bool is_authorize { get; set; } = false;
        public string? authorize_by { get; set; }

        //Relationships
        public virtual HajurKoCarRentalUser HajurKoCarRentalUser { get; set; }
        public virtual Car Car { get; set; }
    }
}
