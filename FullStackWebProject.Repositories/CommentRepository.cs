using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FullStackWebProject.Models;
using FullStackWebProject.RepositoriesContracts;

namespace FullStackWebProject.Repositories;


public class CommentRepository : ICommentRepository
{
	public Task AddCommentAsync(Comment comment)
	{
		throw new NotImplementedException();
	}

	public Task DeleteCommentAsync(int id)
	{
		throw new NotImplementedException();
	}
}
