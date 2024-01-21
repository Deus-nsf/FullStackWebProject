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
	WikY est un wiki en ligne permettant d'ajouter des articles sur des thèmes variés.
	C’est un site responsive avec un design cohérent.

	Modèle

	Article
	• Thème (unique)
	• Auteur (obligatoire) (taille max 30 caractères)
	• Date de création (Date générée automatiquement côté serveur lors de la création)
	• Date de modification (Date générée automatiquement côté serveur lors de la modification)
	• Contenu
	• Liste de commentaires

	Commentaire
	• Auteur (obligatoire)
	• Date de création (Date générée automatiquement côté serveur lors de la création)
	• Date de modification (Date générée automatiquement côté serveur lors de la modification)
	• Contenu (taille max 100 caractères)
	• Un et un seul article

	Structure du projet
	- Un projet d’application ASP.NET Core 8
	- Une architecture MVC avancé (Business + Repository) avec des projets par couche (les Views
	et Controllers restent dans le projet de base)

Travail à faire

	Création des données
	• Créer les classes article et commentaire
	• Ajouter Entity Framework Core
		o Installation des packages NuGets
		o Création du context EF
		o Ajout du context dans les services

	Gestion des articles
	• Pouvoir lister / détail / créer / modifier / supprimer les articles
		-> AddArticleAsync
		-> GetArticlesAsync
		-> GetArticleByIdAsync
		-> UpdateArticleAsync
		-> DeleteArticleAsync

		-> AddCommentAsync
		-> UpdateCommentAsync
		-> DeleteCommentAsync

	Recherche
	• La page d’accueil affiche l’article le plus récent
		o Utiliser une vue partielle pour ré-utiliser le code de l’affichage du détail d’un article
	• Faire une page permettant de rechercher un article (contenu, thème, etc…)

	Gestion des commentaires
	• La liste de commentaire s’affiche dans le détail d’un article
	• Pouvoir ajouter des commentaires de 3 façons différentes :
		o Dans une page dédiée à l’ajout de commentaire pour 1 article (sur le détail de
			l’article on a un bouton « ajouter un commentaire » qui redirige l’utilisateur vers
			une autre page)
		o Directement dans la page de détail de l’article avec un formulaire non typé (avec des
			champs input fait sans helpers) ou un formulaire typé en vue partielle
		o Un autre formulaire dans la page de détail mais cette fois si en AJAX
	• Pouvoir supprimer un commentaire via un lien normal et via un lien en AJAX

	• BONUS
		o Faire une pagination et un tri des articles et commentaires

	Comment to test Azure DevOps CI Tests trigger.

 */