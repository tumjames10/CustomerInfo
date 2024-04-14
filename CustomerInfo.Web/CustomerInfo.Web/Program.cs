using CustomerInfo.Web.Config;
using CustomerInfo.Web.Services;
using CustomerInfo.Web.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<ConfigSettings>(builder.Configuration.GetSection(nameof(ConfigSettings)));
var connectionString = builder.Configuration.GetConnectionString("CustomerDatabase");
builder.Services.AddScoped<ICustomerServiceClient, CustomerServiceClient>();
builder.Services.AddTransient<HttpClient>();

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
