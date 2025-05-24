using BuecherVerwaltungEmpty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuecherVerwaltungEmpty.Controllers;

public class BuecherController : Controller
{
    private readonly AppDbContext _context;

    public BuecherController(AppDbContext context)
    {
        _context = context;
    }

    // Nur ungelesene Bücher anzeigen
    public async Task<IActionResult> Index()
    {
        var buecher = await _context.Buecher
            .Where(b => !b.IstGelesen)
            .ToListAsync();
        return View(buecher);
    }

    // Neue Buch-Ansicht (GET)
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Neues Buch speichern (POST)
    [HttpPost]
    public async Task<IActionResult> Create(Buch buch)
    {
        if (ModelState.IsValid)
        {
            _context.Add(buch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(buch);
    }

    // Buch als gelesen markieren
    [HttpPost]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var buch = await _context.Buecher.FindAsync(id);
        if (buch != null)
        {
            buch.IstGelesen = true;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // Gelesene Bücher anzeigen
    public async Task<IActionResult> DontRead(string searchString, int? selectedYear, int? minRating)
    {
        var buecherQuery = _context.Buecher.Where(b => b.IstGelesen);

        // Freitextsuche
        if (!string.IsNullOrEmpty(searchString))
        {
            buecherQuery = buecherQuery.Where(b =>
                b.Titel.Contains(searchString) ||
                b.Autor.Contains(searchString) ||
                b.Verlag.Contains(searchString));
        }

        // Jahrfilter
        if (selectedYear.HasValue)
        {
            buecherQuery = buecherQuery.Where(b => b.Jahr == selectedYear);
        }

        // Mindestbewertung
        if (minRating.HasValue)
        {
            buecherQuery = buecherQuery.Where(b => b.Bewertung >= minRating);
        }

        // Jahr-Auswahl vorbereiten
        var jahre = await _context.Buecher
            .Where(b => b.IstGelesen)
            .Select(b => b.Jahr)
            .Distinct()
            .OrderByDescending(j => j)
            .ToListAsync();

        ViewBag.Jahre = jahre;
        ViewData["CurrentFilter"] = searchString;
        ViewData["SelectedYear"] = selectedYear;
        ViewData["MinRating"] = minRating;

        return View(await buecherQuery.ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var buch = await _context.Buecher.FindAsync(id);
        if (buch == null)
        {
            return NotFound();
        }
        return View(buch);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Buch buch)
    {
        if (ModelState.IsValid)
        {
            _context.Update(buch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(buch);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var buch = await _context.Buecher.FindAsync(id);
        if (buch != null)
        {
            _context.Buecher.Remove(buch);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateRating(int id, int rating)
    {
        var buch = await _context.Buecher.FindAsync(id);
        if (buch == null)
        {
            return NotFound();
        }

        buch.Bewertung = rating;
        await _context.SaveChangesAsync();

        return Ok(); 
    }


}