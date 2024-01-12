var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

 */