using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Carros.Models;
using Carros.Services;

namespace Carros.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarService _carService;
    public HomeController(ILogger<HomeController> logger, ICarService carService)
    {
        _logger = logger;
        _carService = carService;
    }
    public IActionResult Index(string tipo)
    {
        var cars = _carService.GetCarrosDto();
        ViewData["filter"] = string.IsNullOrEmpty(tipo) ? "all" : tipo;
        return View(cars);
    }
    public IActionResult Details(int Numero)
    {
        var carro = _carService.GetDetailedCarro(Numero);
        carro.Tipos = _carService.GetTipos();
        return View(carro);
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier });
    }
}
