using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FullStackWebProject.Models;


namespace FullStackWebProject.RepositoriesContracts;


public interface ICommentRepository
{
	public /*async*/ Task AddCommentAsync(Comment comment);
	public /*async*/ Task UpdateCommentAsync(Comment comment);
	public /*async*/ Task DeleteCommentAsync(int id);
}
