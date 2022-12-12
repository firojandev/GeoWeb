using System;
using GEOAttendance.Extensions;
using GEOAttendance.Services;
using GeoService.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


ConfigurationManager configuration = builder.Configuration;

builder.Services.AddMqttClientHostedService();

builder.Services.AddTransient<IGeoPublisher, GeoPublisher>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserLocation, UserLocation>();
builder.Services.AddTransient<IAnnouceMessageService, AnnouceMessageService>();
builder.Services.AddTransient<IUserImageService, UserImageService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Users}/{id?}");

app.Run();

