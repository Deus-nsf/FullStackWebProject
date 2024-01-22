using System.Diagnostics;

using FullStackWebProject.Models;
using FullStackWebProject.ServicesContracts;
using FullStackWebProject.UserInterface.Models;

using Microsoft.AspNetCore.Mvc;


namespace FullStackWebProject.UserInterface.Controllers;


public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IArticleService _articleService;

	public HomeController(ILogger<HomeController> logger,
							IArticleService articleService)
	{
		_logger = logger;
		_articleService = articleService;
	}


	public async Task<IActionResult> Index()
	{
		// Not very optimized on the first call,
		// but EF Core will cache the list of entities for later :)
		List<Article> articles = await _articleService.GetArticles();
		Article article = articles.OrderByDescending(a => a.ModificationDate)
									.FirstOrDefault() ?? new Article();

		return View(article);
	}


	[ResponseCache(Duration = 0,
					Location = ResponseCacheLocation.None,
					NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
		});
	}
}
