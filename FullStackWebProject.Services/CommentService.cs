using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FullStackWebProject.Models;
using FullStackWebProject.RepositoriesContracts;
using FullStackWebProject.ServicesContracts;

namespace FullStackWebProject.Services;


public class CommentService : ICommentService
{
	private readonly ICommentRepository _commentRepository;
	public CommentService(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	public async Task AddComment(Comment comment)
	{
		await _commentRepository.AddCommentAsync(comment);
	}


	public async Task DeleteComment(int id)
	{
		await _commentRepository.DeleteCommentAsync(id);
	}


	public async Task UpdateComment(Comment comment)
	{
		await _commentRepository.UpdateCommentAsync(comment);
	}
}
