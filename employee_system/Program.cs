using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using employee_system;
using employee_system.Data;



var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration); // My custom startup class.


builder.Services.AddTransient<employee_system.IEmailSender, EmailSender>();




var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

startup.ConfigureServices(builder.Services); // Add services to the container.


var app = builder.Build();

startup.Configure(app, app.Environment); // Configure the HTTP request pipeline.


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();

