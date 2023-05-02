using HajurKoCarRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Controllers;

public class CarControllers : Controller
{
    //getting database context to the controller
    private readonly HajurKoCarRentalContext _context;

    //defining a constructor
    public CarControllers(HajurKoCarRentalContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var allCars = from cars in _context.Cars
                         join car in _context.Cars on cars.CarId equals car.CarId
                         select new
                         {
                             car.CarId,
                             car.CarName,
                             car.CarImage,
                             car.CarModel,
                             car.CarNumber,
                             car.is_available,
                             car.CarBrand,
                             car.RentPrice
                             
                         };
        return View(await allCars.ToListAsync());
    }
}
