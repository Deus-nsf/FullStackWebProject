﻿using Bogus;

using FullStackWebProject.Models;
using FullStackWebProject.ServicesContracts;
using FullStackWebProject.UserInterface.ViewModels;

using Microsoft.AspNetCore.Mvc;


namespace FullStackWebProject.UserInterface.Controllers;


public class CommentController : Controller
{
	private readonly IArticleService _articleService;
	private readonly ICommentService _commentService;
	public CommentController(IArticleService articleService, 
							ICommentService commentService)
	{
		_articleService = articleService;
		_commentService = commentService;
	}


	// ----------- ACTIONS -----------


	public IActionResult Index()
	{
		return View();
	}


	[HttpGet]
	public async Task<IActionResult> CreateComment(int articleId)
	{
#if DEBUG
		await TestAddComment(articleId);
		return RedirectToAction("DisplayComments", 
								new { ArticleId = articleId });
#else
		await TestAddComment(articleId);
		return RedirectToAction("DisplayComments", 
								new { ArticleId = articleId });
		//return View(); // I need to create an actual form
#endif
	}
	//[HttpPost]
	//public async Task<IActionResult> CreateComment(Comment comment)
	//{
	//	return View();
	//}



	public async Task<IActionResult> DisplayAllComments()
	{
#if DEBUG
		List<Article> articlesWithoutComments = 
			await _articleService.GetArticles();

		List<Comment> comments = new();

		foreach (Article article in articlesWithoutComments)
		{
			Article? articleWithComments = 
				await _articleService.GetArticleById(article.Id);

			comments.AddRange(articleWithComments?.Comments ?? new());
		}

		comments = 
			comments.OrderByDescending(c => c.ModificationDate).ToList();

		ArticleCommentViewModel viewModel = new() 
		{
			Article = null, 
			Comments = comments 
		};
		
		return View("DisplayComments", viewModel);
#else
		ArticleCommentViewModel viewModel = new();
		return View("DisplayComments", viewModel);
#endif
	}



	public async Task<IActionResult> DisplayComments(int articleId)
	{
		Article? article = await _articleService.GetArticleById(articleId);
		List<Comment> comments = article?.Comments.ToList() ?? new();
		ArticleCommentViewModel viewModel = new()
		{
			Article = article,
			Comments = comments
		};

		return View(viewModel);
	}


	public async Task<IActionResult> DeleteComment(int id, int articleId)
	{
		await _commentService.DeleteComment(id);

		return RedirectToAction("DisplayComments", 
								new { ArticleId = articleId });
	}


	// ----------- OTHER -----------


	public async Task TestAddComment(int articleId)
	{
		Comment comment = new()
		{
			CreationDate = DateTime.Now,
			ModificationDate = DateTime.Now,
			Author = new Faker().Person.FullName,
			ArticleId = articleId,  // EF Core will auto wire with the linked entity upon insert
			Content = new Faker().Lorem.Sentences(1)    // May crash due to length constraint
		};

		try
		{
			await _commentService.AddComment(comment);
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync(ex.Message);
			//ModelState.AddModelError(ex.FieldName, ex.Message);
			throw;  // I need a dedicated error page for Content above StringLength(100)
		}
	}
}
