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


public class CommentRepository : ICommentRepository
{
	private readonly WikYDbContext _dbContext;
	public CommentRepository(WikYDbContext dbContext)
	{  
		_dbContext = dbContext;
	}


	public async Task AddCommentAsync(Comment comment)
	{
		await _dbContext.Comments.AddAsync(comment);
		await _dbContext.SaveChangesAsync();
	}


	public async Task UpdateCommentAsync(Comment comment)
	{
		await _dbContext.Comments
			.Where(c => c.Id == comment.Id).ExecuteUpdateAsync
		(
			updates => updates
				.SetProperty(c => c.Content, comment.Content)
				.SetProperty(c => c.ModificationDate, DateTime.Now)
		);
	}


	public async Task DeleteCommentAsync(int id)
	{
		await _dbContext.Comments
			.Where(c => c.Id == id).ExecuteDeleteAsync();
	}
}
