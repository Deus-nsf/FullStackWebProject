using System.Threading.Channels;

using FullStackWebProject.Models;

using Microsoft.EntityFrameworkCore;


namespace FullStackWebProject.Repositories.Context;


public class WikYDbContext : DbContext
{
	// Entities/Models description
	public DbSet<Article> Articles { get; set; }
	public DbSet<Comment> Comments { get; set; }


	public WikYDbContext(DbContextOptions<WikYDbContext> options)
		: base(options) { }


	// DB description
	protected override void OnConfiguring(
		DbContextOptionsBuilder optionsBuilder)
	{
		//optionsBuilder.UseSqlServer
		//(
		//	"Data Source=(localdb)\\MSSQLLOCALDB;" +
		//	"Initial Catalog=WikY;" +
		//	"Integrated Security=True"
		//);

#if DEBUG
		optionsBuilder.LogTo(Console.WriteLine, 
			Microsoft.Extensions.Logging.LogLevel.Debug);
#else
		optionsBuilder.LogTo(Console.WriteLine, 
			Microsoft.Extensions.Logging.LogLevel.None);
#endif

		base.OnConfiguring(optionsBuilder);
	}


	// DB configuration, default/initial data
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		Article article = new Article()
		{
			Id = 1,
			Topic = "Climate change",
			Author = "Deus-nsf",
			CreationDate = DateTime.Now,
			ModificationDate = DateTime.Now,
			Content = "It's a real concern but people shouldn't be so political about it :D \n" +
			"it does a great disservice to a very serious problem."
		};

		Comment comment = new Comment()
		{
			Id = 1,
			Author = "Aurélien Barrau",
			CreationDate = DateTime.Now,
			ModificationDate = DateTime.Now,
			Content = "Your web app IS part of the problem!",

			ArticleId = 1
		};

		modelBuilder.Entity<Article>().HasData(article);
		modelBuilder.Entity<Comment>().HasData(comment);

		base.OnModelCreating(modelBuilder);
	}
}
