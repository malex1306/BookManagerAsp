using BuecherVerwaltungEmpty.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// MVC hinzufügen
builder.Services.AddControllersWithViews();

// DbContext hinzufügen
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=buecher.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Buecher.Any())
    {
        context.Buecher.AddRange(
            new Buch { Titel = "Der Hobbit", Autor = "J.R.R. Tolkien" },
            new Buch { Titel = "1984", Autor = "George Orwell" }
        );
        context.SaveChanges();
    }
}


// MVC-Routen aktivieren
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();