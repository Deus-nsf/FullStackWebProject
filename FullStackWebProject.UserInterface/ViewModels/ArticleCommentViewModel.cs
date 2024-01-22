using FullStackWebProject.Models;

namespace FullStackWebProject.UserInterface.ViewModels;

public class ArticleCommentViewModel
{
	public Article? Article { get; set; }
    public List<Comment> Comments { get; set; } = new();
}
