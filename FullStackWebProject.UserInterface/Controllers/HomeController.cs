using System.Diagnostics;

using FullStackWebProject.ServicesContracts;
using FullStackWebProject.UserInterface.Models;

using Microsoft.AspNetCore.Mvc;

namespace FullStackWebProject.UserInterface.Controllers;
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IArticleService _articleService;

	public HomeController(ILogger<HomeController> logger, IArticleService articleService)
	{
		_logger = logger;
		_articleService = articleService;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		_articleService.GetArticles();
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
