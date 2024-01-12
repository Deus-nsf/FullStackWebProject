using FullStackWebProject.Models;

namespace FullStackWebProject.RepositoriesContracts;


public interface IArticleRepository
{
	public /*async*/ Task<List<Article>> GetArticlesAsync(int? startId = null, int? endId = null);
	public /*async*/ Task<Article> GetArticleByIdAsync(int id);
	public /*async*/ Task AddArticleAsync(Article article);
	public /*async*/ Task UpdateArticleAsync(int id, Article article);
	public /*async*/ Task DeleteArticleAsync(int id);
}
