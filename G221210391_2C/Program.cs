using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using G221210391_2C.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost; Database=web_programming; Username=postgres; Password=123456"));
builder.Services.AddDbContext<AppointmentContext>(options =>
    options.UseNpgsql("Host=localhost; Database=web_programming; Username=postgres; Password=123456"));
builder.Services.AddDbContext<DepartmentContext>(options =>
    options.UseNpgsql("Host=localhost; Database=web_programming; Username=postgres; Password=123456"));
builder.Services.AddDbContext<DoctorContext>(options =>
    options.UseNpgsql("Host=localhost; Database=web_programming; Username=postgres; Password=123456"));
builder.Services.AddDbContext<PatientContext>(options =>
    options.UseNpgsql("Host=localhost; Database=web_programming; Username=postgres; Password=123456"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

