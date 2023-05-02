using System.ComponentModel.DataAnnotations;


namespace HajurKoCarRental.Models;

public class Car
{

    [Key] public int CarId { get; set; }

    [Display(Name = "Car Name")]
    [Required(ErrorMessage = "Car name must be entered")]
    public string? CarName { get; set; }

    [Display(Name = "Car Brand")]
    [Required(ErrorMessage = "Car Brand must be entered")]
    public string? CarBrand { get; set; }

    [Display(Name = "Car Model")]
    [Required(ErrorMessage = "Car Model must be entered")]
    public string? CarModel { get; set; }

    [Display(Name = "Car Number")]
    [Required(ErrorMessage = "Car Number must be entered")]
    public string? CarNumber { get; set; }

    [Display(Name = "Rent price")]
    [Required(ErrorMessage = "Rent Price must be entered")]
    public int? RentPrice { get; set; }

    [Display(Name = "Car description")]
    [Required(ErrorMessage = "Car description must be entered")]
    public string? CarDescription { get; set; }

    [Display(Name = "Car Image Path")]
    [Required(ErrorMessage = "Car image must be entered")]
    public string? CarImage { get; set; }

    [Display(Name = "Car Avaibility")]
    [Required(ErrorMessage = "Car's avaibility must be entered")]
    public bool is_available { get; set; }

    //public ICollection<CarRent> CarRents { get; set; }
}
