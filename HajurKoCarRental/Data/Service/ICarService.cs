using HajurKoCarRental.Models;

namespace HajurKoCarRental.Data.Service
{
    public interface ICarService
    {

        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetCarAsync(int id);
        Task AddAsync(Car car);
        Task<Car> UpdateAsync(int id, Car newCar);
        Task DeleteAsync(int id);
    }
}
