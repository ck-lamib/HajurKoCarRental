using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HajurKoCarRental.Controllers;
using Microsoft.AspNetCore.Identity;

namespace HajurKoCarRental.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HajurKoCarRentalUser class
public class HajurKoCarRentalUser : IdentityUser
{
    public string? Address { get; set; }
    //public FormFile? Document { get; set; }


}

