using HajurKoCarRental.Models;

namespace HajurKoCarRental.Data.Service
{
    public interface ICarRentService
    {
        Task<IEnumerable<CarRent>> GetAllAsync();
        Task<CarRent> GetCarRentAsync(int id);
        Task AddAsync(CarRent carRent);
        Task<CarRent> UpdateAsync(int id, CarRent newCarRent);
        Task DeleteAsync(int id);
    }
}
