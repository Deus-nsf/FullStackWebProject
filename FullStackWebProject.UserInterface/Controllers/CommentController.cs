using FullStackWebProject.Models;
using FullStackWebProject.ServicesContracts;
using Microsoft.AspNetCore.Mvc;

namespace FullStackWebProject.UserInterface.Controllers;


public class CommentController : Controller
{
	private readonly IArticleService _articleService;
	private readonly ICommentService _commentService;

	public CommentController(IArticleService articleService, ICommentService commentService)
	{
		_articleService = articleService;
		_commentService = commentService;
	}

	// ----------- ACTIONS -----------

	public async Task<IActionResult> Index()
	{
#if DEBUG
		await TestGetArticlesAndComments();
#endif
		return View();
	}

	// ----------- OTHER -----------

	public async Task TestGetArticlesAndComments()
	{
		Article? article = await _articleService.GetArticleById(1);
		Comment? comment = article?.Comments.FirstOrDefault();

		await Console.Out.WriteLineAsync(article?.ToString());
		await Console.Out.WriteLineAsync(comment?.ToString());
    }
}
