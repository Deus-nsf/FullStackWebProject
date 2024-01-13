using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FullStackWebProject.Models;

namespace FullStackWebProject.ServicesContracts;


public interface ICommentService
{
	public /*async*/ Task AddComment(Comment comment);
	public /*async*/ Task UpdateComment(Comment comment);
	public /*async*/ Task DeleteComment(int id);
}
