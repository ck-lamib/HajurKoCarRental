using HajurKoCarRental.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HajurKoCarRental.Models
{
    public class CarDamage
    {

        [Key] public int CarDamageId { get; set; }

        [ForeignKey("Id")] public int UserId { get; set; }

        [Display(Name = "Damage Description")]
        [Required(ErrorMessage = "Damage description must be entered")]
        public string? DamageDesc { get; set; }


        public bool is_verified { get; set; } = false;

        [Display(Name = "Verified By")]
        public string? verified_by { get; set; }

        public int? damage_price { get; set; }


        //Relationships
        public virtual HajurKoCarRentalUser HajurKoCarRentalUser { get; set; }

    }
}
