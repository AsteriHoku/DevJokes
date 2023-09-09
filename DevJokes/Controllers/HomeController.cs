using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DevJokes.Models;

namespace DevJokes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _client;

    public HomeController(ILogger<HomeController> logger, HttpClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> DevJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://backend-omega-seven.vercel.app/api/getjoke");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var devJoke = JsonSerializer.Deserialize<List<DevJoke>>(content);
        return View(devJoke?[0]);
    }

    public async Task<IActionResult> GeekJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://geek-jokes.sameerkumar.website/api?format=json");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var geekJoke = JsonSerializer.Deserialize<GeekJoke>(content);
        return View(geekJoke);
    }

    public IActionResult Sources()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}