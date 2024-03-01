using Microsoft.EntityFrameworkCore;
using MvcNetCoreEntityComics.data;
using MvcNetCoreEntityComics.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
//string connectionString = builder.Configuration.GetConnectionString("OracleHospital");

builder.Services.AddTransient<IRepositoryComics, RepositoryComicsSqlServer>();

builder.Services.AddDbContext<ComicContext>(options => options.UseSqlServer(connectionString)); 
//builder.Services.AddDbContext<ComicContext>(options => options.UseOracle(connectionString, options => options.UseOracleSQLCompatibility("11")));

var app = builder.Build();

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
