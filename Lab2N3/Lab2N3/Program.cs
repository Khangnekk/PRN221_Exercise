using Lab2N3;
using Lab2N3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
//using Microsoft.Extensions.DependencyInjection.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddSession();
builder.Services.AddDbContext<NorthwindContext>(
	opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionStrings")));
builder.Services.AddSignalR();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseSession();
app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<CartHub>("/rtCart");
});

app.Run();
