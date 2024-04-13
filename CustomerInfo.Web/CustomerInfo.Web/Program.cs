using CustomerInfo.BusinessLogic;
using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Entity;
using CustomerInfo.Repository;
using CustomerInfo.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("CustomerDatabase");
builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

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
