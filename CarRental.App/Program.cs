using CarRental.App;
using CarRental.Buisness.Classes;
using CarRental.Data.Classes;
using CarRental.Data.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IData, CollectionData>();
builder.Services.AddScoped<BookingProcessor>();
builder.Services.AddScoped<LoadData>();
builder.Services.AddScoped<CustomerInputModel>();
builder.Services.AddScoped<VehicleInputModel>();


await builder.Build().RunAsync();
