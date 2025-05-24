using BuecherVerwaltungEmpty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuecherVerwaltungEmpty;

namespace BuecherVerwaltungEmpty.Controllers;

public class BuecherController : Controller
{
    private readonly AppDbContext _context;

    public BuecherController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var buecher = await _context.Buecher.ToListAsync();
        return View(buecher);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
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
}
