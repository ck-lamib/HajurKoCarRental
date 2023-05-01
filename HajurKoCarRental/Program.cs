using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using HajurKoCarRental.Data.Service;
using HajurKoCarRental.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("HajurKoCarRentalContextConnection") ?? throw new InvalidOperationException("Connection string 'HajurKoCarRentalContextConnection' not found.");
var configuration = builder.Configuration;

//builder.Services.AddDbContext<HajurKoCarRentalContext>(options =>
//    options.UseSqlServer(connectionString));

//// Add services to the container.
//builder.Services.AddMemoryCache();
//builder.Services.AddSession();
//builder.Services.AddControllersWithViews();

//builder.Services.AddDefaultIdentity<HajurKoCarRentalUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<HajurKoCarRentalContext>();


// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<HajurKoCarRentalContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("HajurKoCarRentalContextConnection")));

// For Identity
//builder.Services.AddIdentity<HajurKoCarRentalUser, IdentityRole>()
//    .AddEntityFrameworkStores<HajurKoCarRentalContext>().AddDefaultTokenProviders();

// For Identity
builder.Services.AddIdentity<HajurKoCarRentalUser, IdentityRole>()
    .AddEntityFrameworkStores<HajurKoCarRentalContext>()
    .AddDefaultTokenProviders().AddDefaultUI();

builder.Services.AddScoped<ICarRentService, CarRentService>();
builder.Services.AddScoped<ICarService, CarService>();


// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["X-Access-Token"];
                return Task.CompletedTask;
            }
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.MapRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

builder.Services.AddSession();
app.UseSession();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





// Seed database
ApplicationDBInitilizer.SeedUsersAndRolesAsync(app).Wait();
ApplicationDBInitilizer.Seed(app);


app.Run();
