using FullStackWebProject.Models;
using FullStackWebProject.ServicesContracts;

using Microsoft.AspNetCore.Mvc;
using Bogus;


namespace FullStackWebProject.UserInterface.Controllers;


public class ArticleController : Controller
{
	private readonly IArticleService _articleService;
	public ArticleController(IArticleService articleService)
	{
		_articleService = articleService;
	}


	// ----------- ACTIONS -----------


	public IActionResult Index()
	{
		return View();
	}


	[HttpGet]
	public async Task<IActionResult> CreateArticle()
	{
#if DEBUG
		await TestAddArticle();
		return RedirectToAction("DisplayArticles");
#else
		await TestAddArticle();
		return RedirectToAction("DisplayArticles");
		//return View(); // I need to create an actual form
#endif
	}
	/*HttpPost sur une Web App full stack MVC (pas API) avec des generations d' AntiForgeryToken et des protection [ValidateAntiForgeryToken] sur les routes*/
	//[HttpPost]
	//public async Task<IActionResult> CreateArticle(Article article)
	//{
	//	return View();
	//}


	//public async Task<IActionResult> CreateComment(int id)
	//{
	//	return RedirectToAction("CreateComment", "Comment", new { ArticleId = id });
	//}


	public async Task<IActionResult> DisplayArticles()
	{
		List<Article> articles = await _articleService.GetArticles();
		articles =
			articles.OrderByDescending(a => a.ModificationDate).ToList();

		return View(articles);
	}


	public async Task<IActionResult> DeleteArticle(int id)
	{
		await _articleService.DeleteArticle(id);

		return RedirectToAction("DisplayArticles");
	}


	// ----------- OTHER -----------


	//public async Task TestGetArticles()
	//{
	//	List<Article> articles = await _articleService.GetArticles();

	//	foreach (Article article in articles)
	//	{
	//		await Console.Out.WriteLineAsync(article.ToString());
	//	}
	//}


	public async Task TestAddArticle()
	{
		Article article = new()
		{
			CreationDate = DateTime.Now,
			ModificationDate = DateTime.Now,
			Author = new Faker().Person.FullName,
			Topic = new Faker().Vehicle.Model(),
			Content = new Faker().Lorem.Sentences(5)
		};

		await _articleService.AddArticle(article);
	}
}
