using FullStackWebProject.Models;
using FullStackWebProject.ServicesContracts;
using Microsoft.AspNetCore.Mvc;

namespace FullStackWebProject.UserInterface.Controllers;


public class ArticleController : Controller
{
	private readonly IArticleService _articleService;

	public ArticleController(IArticleService articleService)
	{
		_articleService = articleService;
	}

	// ----------- ACTIONS -----------

	public async Task<IActionResult> Index()
	{
#if DEBUG
		await TestGetArticles();
#endif
		return View();
	}

	// ----------- OTHER -----------

	public async Task TestGetArticles()
	{
		List<Article> articles = await _articleService.GetArticles();

        foreach (Article article in articles)
        {
			await Console.Out.WriteLineAsync(article.ToString());
        }
    }
}
