using Microsoft.EntityFrameworkCore;
using ThemeIntegrationInNet;
using ThemeIntegrationInNet.BussinessService.Concreate;
using ThemeIntegrationInNet.BussinessService.Interface;
using ThemeIntegrationInNet.d;
using ThemeIntegrationInNet.DataEntity;
using ThemeIntegrationInNet.Repositroy.Concreate;
using ThemeIntegrationInNet.Repositroy.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<JobPortalEfContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IJobRepositroy, JobRepositroy>();
builder.Services.AddScoped<IJobTypeRepository, JobTypeRepository>(); 
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IJobTypeService, JobTypeService>();



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
