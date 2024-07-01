using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Personal_Blog.Models
{
	public class Blog
	{
		[Key]
		public int Id { get; set; }

		public string Title { get; set; } = "No title";
		public string Content { get; set; } = string.Empty;
		[Required]
		[NotNull]
		public string Author { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
