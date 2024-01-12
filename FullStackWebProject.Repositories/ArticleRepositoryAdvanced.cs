using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FullStackWebProject.Models;
using FullStackWebProject.RepositoriesContracts;

namespace FullStackWebProject.Repositories;


public class ArticleRepositoryAdvanced : IArticleRepository
{
	public Task AddArticleAsync(Article article)
	{
		throw new NotImplementedException();
	}

	public Task DeleteArticleAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Article> GetArticleByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<List<Article>> GetArticlesAsync(int? startId = null, int? endId = null)
	{
		// this version will have pagination
		throw new NotImplementedException();
	}

	public Task UpdateArticleAsync(int id, Article article)
	{
		throw new NotImplementedException();
	}
}
