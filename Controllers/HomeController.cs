using System;
using System.Text;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RandomPasscode.Models;


namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private int codigoAleatorioNumero = 0;
    private string codigoAleatorioActual = string.Empty;
    
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index(){
        int codigoAleatorioNumero = HttpContext.Session.GetInt32("codigoAleatorioNumero") ?? 0;
        string codigoAleatorioActual = HttpContext.Session.GetString("codigoAleatorioActual");

        ViewBag.CodigoAleatorio = $"Random passcode {codigoAleatorioNumero}";
        ViewBag.CodigoGenerado = codigoAleatorioActual;
        return View();
    }

    [HttpPost]
    public IActionResult GenerateCode(){
        int codigoAleatorioNumero = HttpContext.Session.GetInt32("codigoAleatorioNumero") ?? 0;
        codigoAleatorioNumero++;
        HttpContext.Session.SetInt32("codigoAleatorioNumero", codigoAleatorioNumero);

        string codigoAleatorioActual = GenerateRandomCode(14);
        HttpContext.Session.SetString("codigoAleatorioActual", codigoAleatorioActual);

        return RedirectToAction("Index");
    }

    private string GenerateRandomCode(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder code = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(characters.Length);
            code.Append(characters[index]);
        }
        return code.ToString();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
