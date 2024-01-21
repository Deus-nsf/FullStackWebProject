using FullStackWebProject.Repositories;
using FullStackWebProject.Repositories.Context;
using FullStackWebProject.RepositoriesContracts;
using FullStackWebProject.Services;
using FullStackWebProject.ServicesContracts;


using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Entity Framework Context
builder.Services.AddDbContext<WikYDbContext>
(
	optionsBuilder => optionsBuilder.UseSqlServer
	(
		"Data Source=(localdb)\\MSSQLLOCALDB;" +
		"Initial Catalog=WikY;" +
		"Integrated Security=True"
	)
);

// Repositories
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
// Pagination
//builder.Services.AddScoped<IArticleRepository, ArticleRepositoryAdvanced>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Services
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days.
	// You may want to change this for production scenarios,
	// see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();





/*
 
Contexte
	WikY est un wiki en ligne permettant d'ajouter des articles sur des th�mes vari�s.
	C�est un site responsive avec un design coh�rent.

	Mod�le

	Article
	� Th�me (unique)
	� Auteur (obligatoire) (taille max 30 caract�res)
	� Date de cr�ation (Date g�n�r�e automatiquement c�t� serveur lors de la cr�ation)
	� Date de modification (Date g�n�r�e automatiquement c�t� serveur lors de la modification)
	� Contenu
	� Liste de commentaires

	Commentaire
	� Auteur (obligatoire)
	� Date de cr�ation (Date g�n�r�e automatiquement c�t� serveur lors de la cr�ation)
	� Date de modification (Date g�n�r�e automatiquement c�t� serveur lors de la modification)
	� Contenu (taille max 100 caract�res)
	� Un et un seul article

	Structure du projet
	- Un projet d�application ASP.NET Core 8
	- Une architecture MVC avanc� (Business + Repository) avec des projets par couche (les Views
	et Controllers restent dans le projet de base)

Travail � faire

	Cr�ation des donn�es
	� Cr�er les classes article et commentaire
	� Ajouter Entity Framework Core
		o Installation des packages NuGets
		o Cr�ation du context EF
		o Ajout du context dans les services

	Gestion des articles
	� Pouvoir lister / d�tail / cr�er / modifier / supprimer les articles
		-> AddArticleAsync
		-> GetArticlesAsync
		-> GetArticleByIdAsync
		-> UpdateArticleAsync
		-> DeleteArticleAsync

		-> AddCommentAsync
		-> UpdateCommentAsync
		-> DeleteCommentAsync

	Recherche
	� La page d�accueil affiche l�article le plus r�cent
		o Utiliser une vue partielle pour r�-utiliser le code de l�affichage du d�tail d�un article
	� Faire une page permettant de rechercher un article (contenu, th�me, etc�)

	Gestion des commentaires
	� La liste de commentaire s�affiche dans le d�tail d�un article
	� Pouvoir ajouter des commentaires de 3 fa�ons diff�rentes :
		o Dans une page d�di�e � l�ajout de commentaire pour 1 article (sur le d�tail de
			l�article on a un bouton � ajouter un commentaire � qui redirige l�utilisateur vers
			une autre page)
		o Directement dans la page de d�tail de l�article avec un formulaire non typ� (avec des
			champs input fait sans helpers) ou un formulaire typ� en vue partielle
		o Un autre formulaire dans la page de d�tail mais cette fois si en AJAX
	� Pouvoir supprimer un commentaire via un lien normal et via un lien en AJAX

	� BONUS
		o Faire une pagination et un tri des articles et commentaires

	Comment to test Azure DevOps CI Tests trigger.

 */