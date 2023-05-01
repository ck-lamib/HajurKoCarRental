using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using HajurKoCarRental.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace HajurKoCarRental.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HajurKoCarRentalContext _context;

    public HomeController(ILogger<HomeController> logger, HajurKoCarRentalContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {

        var searchBarValue = HttpContext.Session.GetString("searchBarValue");
        var sortingOrderCol = HttpContext.Session.GetString("sortingOrderCol");
        var stockAvailability = HttpContext.Session.GetString("stockAvailability");

        var dababaseCarList = from car in _context.Cars select car.CarId;

        ViewBag.sortingOrderCol = string.IsNullOrEmpty(sortingOrderCol) ? "na" : sortingOrderCol;
        ViewBag.stockAvailability = string.IsNullOrEmpty(stockAvailability) ? "all" : stockAvailability;

        IEnumerable<HomeCar> carDetails = from allCar in dababaseCarList
                                          join car in _context.Cars on allCar equals car.CarId
                                          //join category in _context.DVDCategories on dvd.CategoryNumber equals category.CategoryNumber
                                          select new HomeCar
                                          {
                                              CarName = car.CarName,
                                              CarDescription = car.CarDescription,
                                              CarImage = car.CarImage,
                                              CarModel = car.CarModel,
                                              CarNumber = car.CarNumber,
                                              isAvailable = car.is_available,
                                              isRented = false,
                                              RentPrice = car.RentPrice
                                          };


        if (!string.IsNullOrEmpty(searchBarValue))
        {
            ViewBag.searchBarValue = searchBarValue;
            carDetails = carDetails.Where(d =>
                d.CarName.ToLower().Contains(searchBarValue.ToLower()) ||
                d.CarModel.ToLower().Contains(searchBarValue.ToLower()));
        }

        // cases for sorting
        switch (sortingOrderCol)
        {
            case "pa":
                carDetails = carDetails.OrderBy(d => d.RentPrice);
                break;

            case "pd":
                carDetails = carDetails.OrderByDescending(d => d.RentPrice);
                break;


            default:
                carDetails = carDetails.OrderBy(d => d.CarName);
                break;
        }


        switch (stockAvailability)
        {
            case "available":
                carDetails = carDetails.Where(d => d.isAvailable == true);
                break;

            case "outOfStock":
                carDetails = carDetails.Where(d => d.isAvailable == false);
                break;
        }



        return View(carDetails);
    }




    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostIndex()
    {
        HttpContext.Session.SetString("searchBarValue", Request.Form["searchBarValue"]);
        HttpContext.Session.SetString("sortingOrderCol", Request.Form["sortingOrderCol"]);
        HttpContext.Session.SetString("stockAvailability", Request.Form["stockAvailability"]);
        return RedirectToAction("Index");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}
