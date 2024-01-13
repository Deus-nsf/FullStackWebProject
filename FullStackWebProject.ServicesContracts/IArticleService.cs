using FullStackWebProject.Models;

namespace FullStackWebProject.ServicesContracts;


public interface IArticleService
{
	public /*async*/ Task AddArticle(Article article);
	public /*async*/ Task<List<Article>> GetArticles(int? startId = null, int? endId = null);
	public /*async*/ Task<Article?> GetArticleById(int id);
	public /*async*/ Task UpdateArticle(Article article);
	public /*async*/ Task DeleteArticle(int id);
}
