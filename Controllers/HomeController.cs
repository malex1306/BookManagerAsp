using Microsoft.AspNetCore.Mvc;

namespace BuecherVerwaltungEmpty.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}