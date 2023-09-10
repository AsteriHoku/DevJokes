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

    [Route("/Dev")]
    public async Task<IActionResult> DevJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://backend-omega-seven.vercel.app/api/getjoke");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var devJoke = JsonSerializer.Deserialize<List<DevJoke>>(content);
        return View(devJoke?[0]);
    }

    [Route("/Geek")]
    public async Task<IActionResult> GeekJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://geek-jokes.sameerkumar.website/api?format=json");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var geekJoke = JsonSerializer.Deserialize<GeekJoke>(content);
        return View(geekJoke);
    }
    
    [Route("/Programming")]
    public async Task<IActionResult> ProgrammingJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://official-joke-api.appspot.com/jokes/programming/random#");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var programmingJoke = JsonSerializer.Deserialize<List<ProgrammingJoke>>(content);
        return View(programmingJoke?[0]);
    }
    
    [Route("/NSFWProgramming")]
    public async Task<IActionResult> NSFWProgrammingJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Programming");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var programmingJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(programmingJoke);
    }

    [Route("/Spooky")]
    public async Task<IActionResult> SpookyJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Spooky");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var spookyJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(spookyJoke);
    }
    
    [Route("/Pun")]
    public async Task<IActionResult> PunJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Pun");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var punJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(punJoke);
    }
    
    [Route("/Misc")]
    public async Task<IActionResult> MiscJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Miscellaneous");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var miscJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(miscJoke);
    }
    
    [Route("/Christmas")]
    public async Task<IActionResult> ChristmasJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Christmas");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var xmasJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(xmasJoke);
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