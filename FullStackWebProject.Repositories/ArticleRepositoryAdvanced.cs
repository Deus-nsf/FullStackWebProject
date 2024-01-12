using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FullStackWebProject.Models;
using FullStackWebProject.Repositories.Context;
using FullStackWebProject.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebProject.Repositories;


public class ArticleRepositoryAdvanced : IArticleRepository
{
	private readonly WikYDbContext _context = new();


	public async Task AddArticleAsync(Article article)
	{
		await _context.Articles.AddAsync(article);
		await _context.SaveChangesAsync();
	}


	public async Task<List<Article>> GetArticlesAsync(int? startId = null, int? endId = null)
	{
		// this version will have pagination later
		return await _context.Articles.ToListAsync();
	}


	public async Task<Article?> GetArticleByIdAsync(int id)
	{
		return await _context.Articles.FindAsync(id);
	}


	public async Task UpdateArticleAsync(Article article)
	{
		await _context.Articles.Where(a => a.Id == article.Id).ExecuteUpdateAsync
		(
			updates => updates.SetProperty(a => a.Topic, article.Topic)
								.SetProperty(a => a.ModificationDate, DateTime.Now)
								.SetProperty(a => a.Content, article.Content)
		);
	}


	public async Task DeleteArticleAsync(int id)
	{
		await _context.Articles.Where(a => a.Id == id).ExecuteDeleteAsync();
	}
}
