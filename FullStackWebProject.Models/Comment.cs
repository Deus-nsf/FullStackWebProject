using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;


namespace FullStackWebProject.Models;


public class Comment
{
	// EF handles Required, Auto Increment, and Primary Key automatically
	// for the property named "Id" with the DBMS
	public int Id { get; set; }

	[Required]
	[StringLength(30)]
	public string Author { get; set; } = string.Empty;

	public DateTime? CreationDate { get; set; }

	public DateTime? ModificationDate { get; set; }

	[Required]
	[StringLength(100)]
	public string Content { get; set; } = string.Empty;

	// 1, 1
	[Required]
	public int ArticleId { get; set; }	// Will become Foreign Key
	// (must always set linked entity to nullable for EF Core)
	public Article? Article { get; set; }	// ORM ONLY


	// ----------- Debug Methods -----------

#if DEBUG
	public override string ToString()
	{
		string myString =
			$"{nameof(Id)} : {Id} \n" +
			$"{nameof(Author)} : {Author} \n" +
			$"{nameof(CreationDate)} : {CreationDate} \n" +
			$"{nameof(ModificationDate)} : {ModificationDate} \n" +
			$"{nameof(Article)} : {Article?.Topic ?? "ERROR"} \n" +
			$"{nameof(Content)} : {Content} \n";

		return myString += "\n\n";
	}
#endif
}