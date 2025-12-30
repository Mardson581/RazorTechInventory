using TechInventory.Services.Device;
using TechInventory.Services.DeviceModel;
using TechInventory.Services.Brand;
using TechInventory.Services.Maintenance;
using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("SQLAZURECONNSTR_AZURE_SQL_CONNECTIONSTRING")));

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDeviceModelService, DeviceModelService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IMaintenanceRecordService, MaintenanceRecordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
