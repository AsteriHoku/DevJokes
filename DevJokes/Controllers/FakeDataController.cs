using System.Text.Json;
using DevJokes.Models;
using DevJokes.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevJokes.Controllers;

public class FakeDataController : Controller
{
    private readonly HttpClient _client;

    public FakeDataController(HttpClient client)
    {
        _client = client;
    }

    [Route("/FakeData/FakePerson")]
    public async Task<IActionResult> FakePerson()
    {
        var fakePeeps = new FakePersonVM[5];
        for (int i = 0; i < 5; ++i)
        {
            _client.DefaultRequestHeaders.Clear();
            var uri = new Uri("https://randomuser.me/api/");
            var httpResponse = await _client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<FakePersonResult>(content);
            var dob = res?.results[0].dob.date.Substring(0, 10);
            if (res != null) res.results[0].dob.date = dob;
            fakePeeps[i] = res.results[0];
        }

        ViewBag.peeps = fakePeeps;
        return View();
    }
}