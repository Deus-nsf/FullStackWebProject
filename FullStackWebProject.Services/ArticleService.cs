using FullStackWebProject.Models;
using FullStackWebProject.RepositoriesContracts;
using FullStackWebProject.ServicesContracts;

namespace FullStackWebProject.Services;


public class ArticleService : IArticleService
{
	private readonly IArticleRepository _articleRepository;
	public ArticleService(IArticleRepository articleRepository)
	{
		_articleRepository = articleRepository;
	}


	public async Task AddArticle(Article article)
	{
		await _articleRepository.AddArticleAsync(article);
	}


	public async Task<List<Article>> GetArticles(int? startId = null, int? endId = null)
	{
		return await _articleRepository.GetArticlesAsync(startId, endId);
	}


	public async Task<Article?> GetArticleById(int id)
	{
		return await _articleRepository.GetArticleByIdAsync(id);
	}


	public async Task UpdateArticle(Article article)
	{
		await _articleRepository.UpdateArticleAsync(article);
	}


	public async Task DeleteArticle(int id)
	{
		await _articleRepository.DeleteArticleAsync(id);
	}
}
