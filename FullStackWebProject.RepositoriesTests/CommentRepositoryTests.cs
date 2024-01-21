using Microsoft.VisualStudio.TestTools.UnitTesting;
using FullStackWebProject.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullStackWebProject.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using FullStackWebProject.Models;
using Bogus;


namespace FullStackWebProject.Repositories.Tests;


[TestClass()]
public class CommentRepositoryTests
{
	private DbContextOptionsBuilder<WikYDbContext> _optionsBuilder =
		new DbContextOptionsBuilder<WikYDbContext>().UseSqlServer
		(
			"Data Source=(localdb)\\MSSQLLOCALDB;" +
			"Initial Catalog=WikY_TEST;" +
			"Integrated Security=True"
		);

	private WikYDbContext _dbContextTest;


	public CommentRepositoryTests()
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
	/// I want to ensure the insertion of a comment to an article works
	/// This is used to ensure any migration did not break entity relations
	/// </summary>
	/// <returns></returns>
	[TestMethod()]
	[Timeout(3000)]
	public async Task AddCommentAsyncTest()
	{
		// Arrange
		await VerifyDatabaseConnection();
		CommentRepository commentRepository = new(_dbContextTest);
		int articleId = 1;
		bool insertSuccess = true;
		Comment comment = new()
		{
			CreationDate = DateTime.Now,
			ModificationDate = DateTime.Now,
			Author = new Faker().Person.FullName,

			// May crash due to length constraint
			Content = new Faker().Lorem.Sentences(1),

			// EF Core will auto wire this entity with the linked entity upon insert
			ArticleId = articleId
		};

		// Act
		try
		{
			await commentRepository.AddCommentAsync(comment);
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
}