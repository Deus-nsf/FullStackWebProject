using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FullStackWebProject.Models;


public class Article
{
	// EF handles Required, Auto Increment, and Primary Key automatically
	// for the property named "Id" with the DBMS
    public int Id { get; set; }

	[Remote("CheckUniqueTopic", "Topic", ErrorMessage = "This topic has already been taken")]
    public string? Topic { get; set; }

	[Required]
	[StringLength(30)]
    public string Author { get; set; } = string.Empty;

	public DateTime? CreationDate { get; set; }

	public DateTime? ModificationDate { get; set; }

	[Required]
	[StringLength(45100)]
	public string Content { get; set; } = string.Empty;


	// ----------- Foreign Keys -----------


	// 0, n
	public List<Comment?> Comments { get; set; } = new();


	// ----------- Debug Methods -----------

#if DEBUG
	public override string ToString()
	{
		string myString =
			$"{nameof(Id)} : {Id} \n" +
			$"{nameof(Topic)} : {Topic} \n" +
			$"{nameof(Author)} : {Author} \n" +
			$"{nameof(CreationDate)} : {CreationDate} \n" +
			$"{nameof(ModificationDate)} : {ModificationDate} \n" +
			$"{nameof(Content)} : {Content} \n" +
			$"{nameof(Comments)} from : \n";

		//Comments.ForEach(c => myString += "\n\n" + c.ToString());
		Comments.ForEach(c =>
		{
			myString += $"{nameof(c.Author)} : {c.Author} \n";
		});

		return myString += "\n\n";
	}
#endif
}
