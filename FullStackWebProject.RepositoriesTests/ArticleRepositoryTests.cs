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


	private async Task VerifyDatabase()
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

			Console.WriteLine(ex);
		}

		if(canConnect == false)
		{
			// We need to create the database test copy
			_dbContextTest.Database.Migrate(); // EnsureCreated with migrations

			// This will NOT copy the entities of the real database,
			// only default values declared in the context,
			// to effectively copy the entities, a manual process
			// with the database manager is immensely preferable
		}
	}


	[TestMethod()]
	[Timeout(3000)]
	public async Task GetArticlesAsyncTest()
	{
		// Arrange
		await VerifyDatabase();
		ArticleRepository articleRepository = new(_dbContextTest);

		// Act
		List<Article> articles = await articleRepository.GetArticlesAsync();

		// Assert
		Assert.IsTrue(articles.Count > 0);
	}
}