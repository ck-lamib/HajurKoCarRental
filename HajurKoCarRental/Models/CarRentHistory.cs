using HajurKoCarRental.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HajurKoCarRental.Models
{
    public class CarRentHistory
    {
        [Key] public int CarRentHistoryId { get; set; }

         public int CarRentId { get; set; }

        public bool is_fined { get; set; } = false;
        public string? FinedType { get; set; }


        public int? FinedPrice { get; set; }

        public int? TotalRentPrice { get; set; }
        public int? RentPrice { get; set; }

        //Relationships
        //public virtual HajurKoCarRentalUser HajurKoCarRentalUser { get; set; }
    }
}
