using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Areas.Identity.Data;

public sealed class HajurKoCarRentalContext : IdentityDbContext<HajurKoCarRentalUser>
{
  
    public HajurKoCarRentalContext(DbContextOptions<HajurKoCarRentalContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarRent> CarRents { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    //    modelBuilder.Entity<CarRent>().HasKey(cm => new
    //    {
    //        cm.UserId,
    //        cm.CarId,
    //    });
    //    modelBuilder.Entity<CarRent>().HasOne(a => a.Car).WithMany(cm => cm.CarRents)
    //.HasForeignKey(a => a.CarId);
        //modelBuilder.Entity<CastMember>().HasOne(a => a.DVDTitle).WithMany(cm => cm.CastMembers)
        //    .HasForeignKey(a => a.DVDNumber);

        base.OnModelCreating(modelBuilder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    internal Task GetCarRentAsync(int id)
    {
        throw new NotImplementedException();
    }
}
