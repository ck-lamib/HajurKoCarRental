using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Data.Service
{
    public class CarRentService: ICarRentService
    {

        private readonly HajurKoCarRentalContext _context;

        public CarRentService(HajurKoCarRentalContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CarRent carRent)
        {
            await _context.CarRents.AddAsync(carRent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.CarRents.FirstOrDefaultAsync(n => n.CarRentId == id);
            _context.CarRents.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<CarRent> GetCarRentAsync(int id)
        {
            var result = await _context.CarRents.FirstOrDefaultAsync(n => n.CarRentId == id);
            return result;
        }

        public async Task<IEnumerable<CarRent>> GetAllAsync()
        {
            var result = await _context.CarRents.ToListAsync();
            return result;
        }

        public async Task<CarRent> UpdateAsync(int id, CarRent newCarRent)
        {
            _context.Update(newCarRent);
            await _context.SaveChangesAsync();
            return newCarRent;
        }

    }
}




//using HajurKoCarRental.Areas.Identity.Data;
//using HajurKoCarRental.Models;
//using Microsoft.EntityFrameworkCore;

//namespace HajurKoCarRental.Areas.Identity.Service
//{
//    public class CarRentHistoryServices : ICarRentHistoryService
//    {
//        private readonly HajurKoCarRentalContext _context;

//        public CarRentHistoryServices(HajurKoCarRentalContext context)
//        {
//            _context = context;
//        }

//        public async Task AddAsync(CarRentHistory carRentHistory)
//        {
//            await _context.CarRentHistory.AddAsync(carRentHistory);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var result = await _context.CarRentHistory.FirstOrDefaultAsync(n => n.CarRentHistoryId == id);
//            _context.CarRentHistory.Remove(result);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<CarRentHistory> GetDVDCategoryAsync(int id)
//        {
//            var result = await _context.CarRentHistory.FirstOrDefaultAsync(n => n.CarRentHistoryId == id);
//            return result;
//        }

//        public async Task<IEnumerable<CarRentHistory>> GetAllAsync()
//        {
//            var result = await _context.CarRentHistory.ToListAsync();
//            return result;
//        }

//        public async Task<CarRentHistory> UpdateAsync(int id, CarRentHistory newCarRentHistory)
//        {
//            _context.Update(newCarRentHistory);
//            await _context.SaveChangesAsync();
//            return newCarRentHistory;
//        }

//        public Task<CarRentHistory> GetCarRentHistoryAsync(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

//using HajurKoCarRental.Models;

//namespace HajurKoCarRental.Areas.Identity.Service
//{
//    public interface ICarRentHistoryService
//    {
//        Task<IEnumerable<CarRentHistory>> GetAllAsync();
//        Task<CarRentHistory> GetCarRentHistoryAsync(int id);
//        Task AddAsync(CarRentHistory carRentHistory);
//        Task<CarRentHistory> UpdateAsync(int id, CarRentHistory newCarRenHistory);
//        Task DeleteAsync(int id);
//    }
//}
