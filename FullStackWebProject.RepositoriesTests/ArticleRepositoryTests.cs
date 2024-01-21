using Microsoft.VisualStudio.TestTools.UnitTesting;
using FullStackWebProject.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullStackWebProject.Repositories.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FullStackWebProject.Models;
using Bogus;


namespace FullStackWebProject.Repositories.Tests;


/// <summary>
/// I could test a database in memory, 
/// but for this project I want to be as close as possible
/// to the real thing, so I will test on a copy of the 
/// database if it doesn't exist, and test on that copy
/// </summary>
[TestClass()]
public class ArticleRepositoryTests
{
	private DbContextOptionsBuilder<WikYDbContext> _optionsBuilder =
		new DbContextOptionsBuilder<WikYDbContext>().UseSqlServer
		(
			"Data Source=(localdb)\\MSSQLLOCALDB;" +
			"Initial Catalog=WikY_TEST;" +
			"Integrated Security=True"
		);

	private WikYDbContext _dbContextTest;


	public ArticleRepositoryTests()
	{
		_dbContextTest = new(_optionsBuilder.Options);
	}


	private async Task VerifyDatabaseConnection()
	{
		bool canConnect = false;

		try
		{
			// We try to see if the test database exists and can be contacted
			canConnect = await _dbContextTest.Database.CanConnectAsync();
		}
		catch (Exception ex)
		{
			while (ex.InnerException is not null)
				ex = ex.InnerException;

			await Console.Out.WriteLineAsync(ex.Message);
		}

		if (canConnect == false)
		{
			// We need to create the database test copy
			// EnsureCreated with migrations
			await _dbContextTest.Database.MigrateAsync();

			// This will NOT copy the entities of the real database,
			// only default values declared in the context,
			// to effectively copy the entities, a manual process
			// with the database manager is immensely preferable
		}
	}


	/// <summary>
	/// I want to ensure the ability of connecting to the database
	/// and retrieve at least some data in any shape or form
	/// </summary>
	/// <returns></returns>
	[TestMethod()]
	[Timeout(3000)]
	public async Task GetArticlesAsyncTest()
	{
		// Arrange
		await VerifyDatabaseConnection();
		ArticleRepository articleRepository = new(_dbContextTest);

		// Act
		List<Article> articles = await articleRepository.GetArticlesAsync();

		// Assert
		Assert.IsTrue(articles.Count > 0);
	}


	/// <summary>
	/// I want to ensure the insertion of an article in the database
	/// This will simulate a form validation with Bogus/Faker
	/// </summary>
	/// <returns></returns>
	[TestMethod()]
	[Timeout(3000)]
	public async Task AddArticleAsyncTest()
	{
		// Arrange
		await VerifyDatabaseConnection();
		ArticleRepository articleRepository = new(_dbContextTest);
		Article article = new()
		{
			CreationDate = DateTime.Now,
			ModificationDate = DateTime.Now,
			Author = new Faker().Person.FullName,
			Topic = new Faker().Vehicle.Model(),
			Content = new Faker().Lorem.Sentences(5)
		};
		bool insertSuccess = true;

		// Act
		try
		{
			await articleRepository.AddArticleAsync(article);
		}
		catch (Exception ex)
		{
			while (ex.InnerException is not null)
				ex = ex.InnerException;

			await Console.Out.WriteLineAsync(ex.Message);
			insertSuccess = false;
		}

		// Assert
		Assert.IsTrue(insertSuccess);
	}


	/// <summary>
	/// I want to ensure that I can retrieve the list of comments
	/// attached to the first article with the .Include EF Core function
	/// This is used to ensure any migration did not break entity relations
	/// </summary>
	/// <returns></returns>
	[TestMethod()]
	[Timeout(3000)]
	public async Task GetArticleByIdAsyncTest()
	{
		// Arrange
		await VerifyDatabaseConnection();
		ArticleRepository articleRepository = new(_dbContextTest);

		// Act
		Article? article = await articleRepository.GetArticleByIdAsync(1);
		List<Comment> comments = article?.Comments.ToList() ?? new();

		await Console.Out.WriteLineAsync(article?.ToString() ?? "No Articles");
		foreach (Comment comment in comments)
		{
			await Console.Out.WriteLineAsync(comment.ToString());
		}

		// Assert
		Assert.IsTrue(article is not null);
		Assert.IsTrue(comments.Count > 0);
	}
}