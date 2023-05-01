using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Data.Service;

public class CarService: ICarService
{
    private readonly HajurKoCarRentalContext _context;

    public CarService(HajurKoCarRentalContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _context.Cars.FirstOrDefaultAsync(n => n.CarId == id);
        _context.Cars.Remove(result);
        await _context.SaveChangesAsync();
    }

    public async Task<Car> GetCarAsync(int id)
    {
        var result = await _context.Cars.FirstOrDefaultAsync(n => n.CarId == id);
        return result;
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        var result = await _context.Cars.ToListAsync();
        return result;
    }

    public async Task<Car> UpdateAsync(int id, Car newCar)
    {
        _context.Update(newCar);
        await _context.SaveChangesAsync();
        return newCar;
    }
}