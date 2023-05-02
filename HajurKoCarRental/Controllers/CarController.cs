using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using Microsoft.Extensions.Hosting;

namespace HajurKoCarRental.Controllers
{
    public class CarController : Controller
    {
        private readonly HajurKoCarRentalContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CarController(HajurKoCarRentalContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
              return _context.Cars != null ? 
                          View(await _context.Cars.ToListAsync()) :
                          Problem("Entity set 'HajurKoCarRentalDbContext.CarInfo'  is null.");
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var carInfo = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (carInfo == null)
            {
                return NotFound();
            }

            return View(carInfo);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,CarName,CarBrand,CarImage,CarModel, CarNumber,RentPrice,CarDescription, is_available")] Car carInfo)
        {
            if (ModelState.IsValid)
            {

                _context.Cars.Add(carInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carInfo);
        }



        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var carInfo = await _context.Cars.FindAsync(id);
            if (carInfo == null)
            {
                return NotFound();
            }
            return View(carInfo);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,CarName,CarBrand,CarImage,CarModel, CarNumber,RentPrice,CarDescription, is_available")] Car carInfo)
        {
            if (id != carInfo.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCar = await _context.Cars.FindAsync(id);

                    // Delete the previous image file
                    //if (carImageFile != null && !string.IsNullOrEmpty(existingCar.CarImage))
                    //{
                    //    var existingImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", existingCar.CarImage);
                    //    if (System.IO.File.Exists(existingImagePath))
                    //    {
                    //        System.IO.File.Delete(existingImagePath);
                    //    }
                    //}

                    // Update the car information
                    _context.Entry(existingCar).CurrentValues.SetValues(carInfo);

                    // Check if a new image file is uploaded
                    //if (carImageFile != null)
                    //{
                    //    // Generate a unique file name for the new image
                    //    var uniqueFileName = GetUniqueFileName(carImageFile.FileName);

                    //    // Save the new image file to the wwwroot/images directory
                    //    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);
                    //    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    //    {
                    //        await carImageFile.CopyToAsync(fileStream);
                    //    }

                    //    // Update the car's image file name
                    //    existingCar.CarImage = uniqueFileName;
                    //}

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarInfoExists(carInfo.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(carInfo);
        }


        // Generate a unique file name for the image
        //private string GetUniqueFileName(string fileName)
        //{
        //    fileName = Path.GetFileName(fileName);
        //    return Path.GetFileNameWithoutExtension(fileName)
        //        + "_"
        //        + Guid.NewGuid().ToString().Substring(0, 4)
        //        + Path.GetExtension(fileName);
        //}


        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var carInfo = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (carInfo == null)
            {
                return NotFound();
            }

            return View(carInfo);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'HajurKoCarRentalDbContext.CarInfo'  is null.");
            }
            var carInfo = await _context.Cars.FindAsync(id);
            if (carInfo != null)
            {
                _context.Cars.Remove(carInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarInfoExists(int id)
        {
          return (_context.Cars?.Any(e => e.CarId == id)).GetValueOrDefault();
        }
    }
}
