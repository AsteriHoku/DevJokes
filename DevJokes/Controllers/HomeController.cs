using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using DevJokes.Models;
using DevJokes.Services;

namespace DevJokes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _client;
    private readonly DevService _devService;

    public HomeController(ILogger<HomeController> logger, HttpClient client, DevService devService)
    {
        _logger = logger;
        _client = client;
        _devService = devService;
    }

    //TODO
    //unit tests
    //sweet alert for notify?
    //combine models into 1 VM
    //port logic to service layer
    //port hard lists to models

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [Route("/Dev")]
    public async Task<IActionResult> DevJoke()
    {
        DevJoke devJoke;
        int rand = new Random().Next(0, 2);
        if (rand == 1)
        {
            _client.DefaultRequestHeaders.Clear();
            var uri = new Uri("https://backend-omega-seven.vercel.app/api/getjoke");
            var httpResponse = await _client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            devJoke = JsonSerializer.Deserialize<List<DevJoke>>(content)?[0];
        }
        else
        {
            devJoke = await _devService.NonAPIDevJoke();
        }

        ViewData["Message"] = "Hello from ViewData!";
        ViewBag.binary = await _devService.GetSetDevVM(devJoke.punchline);
        ViewBag.question = devJoke.question;
        ViewBag.punchline = devJoke.punchline;
        ViewBag.aspaction = "DevJoke";

        MemoryStream imageStream = await _devService.GenerateDevJokeCard(devJoke);
        ViewBag.JokeCard = imageStream;

        return View();
    }

    [Route("/Geek")]
    public async Task<IActionResult> GeekJoke()
    {
        GeekJoke geekJoke;
        int rand = new Random().Next(0, 2);
        if (rand == 1)
        {
            _client.DefaultRequestHeaders.Clear();
            var uri = new Uri("https://geek-jokes.sameerkumar.website/api?format=json");
            var httpResponse = await _client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            geekJoke = JsonSerializer.Deserialize<GeekJoke>(content);
        }
        else
        {
            geekJoke = await _devService.NonAPIGeekJoke();
        }
        
        MemoryStream imageStream = await _devService.GenerateGeekJokeCard(geekJoke);
        ViewBag.JokeCard = imageStream;

        return View(geekJoke);
    }

    [Route("/Programming")]
    public async Task<IActionResult> ProgrammingJoke()
    {
        _client.DefaultRequestHeaders.Clear();
        var uri = new Uri("https://official-joke-api.appspot.com/jokes/programming/random#");
        var httpResponse = await _client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var programmingJoke = JsonSerializer.Deserialize<List<ProgrammingJoke>>(content)?[0];
        programmingJoke.binary = await _devService.GetSetDevVM(programmingJoke.punchline);
        return View(programmingJoke);
    }

    [Route("/NSFWProgramming")]
    public async Task<IActionResult> NSFWProgrammingJoke()
    {
        NSFWjoke programmingJoke;
        int rand = new Random().Next(0, 6);
        if (rand == 1)
        {
            programmingJoke = await _devService.NonAPINSFWProgrammingJoke();
        }
        else
        {
            _client.DefaultRequestHeaders.Clear();
            var uri = new Uri("https://v2.jokeapi.dev/joke/Programming");
            var httpResponse = await _client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            programmingJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        }

        var binIn = programmingJoke?.joke ?? programmingJoke?.delivery;
        programmingJoke.lang = await _devService.GetSetDevVM(binIn);
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