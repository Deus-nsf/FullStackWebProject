using Bogus;
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
		return RedirectToAction("DisplayArticles");
	}


	public async Task<IActionResult> DisplayArticles()
	{
		List<Article> articles = await _articleService.GetArticles();
		articles = articles.OrderByDescending(a => a.ModificationDate).ToList();

		return View(articles);
	}


	[HttpGet]
	public async Task<IActionResult> CreateArticle()
	{
#if DEBUG
		await TestAddArticle();
#else
		//return View(); // I need to create an actual form
#endif
		return RedirectToAction("Index");
	}
	//[HttpPost]
	//public async Task<IActionResult> CreateArticle(Article article)
	//{
	//	return View();
	//}


	//public async Task<IActionResult> CreateComment(int id)
	//{
	//	return RedirectToAction("CreateComment", "Comment", new { ArticleId = id });
	//}


	public async Task<IActionResult> DeleteArticle(int id)
	{
		await _articleService.DeleteArticle(id);

		return RedirectToAction("Index");
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
