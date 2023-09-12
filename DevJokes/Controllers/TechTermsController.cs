using DevJokes.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevJokes.Controllers;

public class TechTermsController : Controller
{
    public TechTermsController()
    {
        
    }

    [Route("/TechTerms")]
    public async Task<IActionResult> TechTerms()
    {
        var techVM = new TechTermVM();
        return View(techVM);
    }
}