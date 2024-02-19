using ExerciseRazerPage.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<NorthwindContext>(
	opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionStrings")));

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
