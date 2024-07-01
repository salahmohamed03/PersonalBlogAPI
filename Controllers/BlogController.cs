using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Blog.Data;
using Personal_Blog.Models;

namespace Personal_Blog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly AppDbContext _db;
		public BlogController(AppDbContext db)
		{
			_db = db;
		}
		[HttpPost("CreateBlog")]
		public async Task<IActionResult> CreateBlog(Blog blog)
		{
			if (blog.Author == null)
			{
				return BadRequest("Auther property is required");
			}
			await _db.Blogs.AddAsync(blog);
			await _db.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("GetBlog")] 
		public async Task<IActionResult> GetBlog([FromQuery] int Id)
		{
			var blog = await _db.Blogs.FirstOrDefaultAsync(b => b.Id == Id);
			if(blog  == null)
			{
				return NotFound();
			}
			return Ok(blog);
		}
		[HttpGet("GetBlogsByAuthor")]
		public IActionResult GetBlogsByAuthor(string author)
		{
			var blog = _db.Blogs.Where(b => b.Author == author);
			if (blog == null)
			{
				return NotFound();
			}
			return Ok(blog);
		}

		public record BlogUpdate(int id , string? content,string? title);
		[HttpPut("EditBlog")]
		public async Task<IActionResult> EditBlog(BlogUpdate blogUpdate)
		{
			var blog =await _db.Blogs.FirstOrDefaultAsync(b => b.Id == blogUpdate.id);
			if(blog is null)
			{
				return NotFound();
			}
			blog.Content = blogUpdate.content??blog.Content;
			blog.Title = blogUpdate.title?? blog.Title ;
			_db.Update(blog);
			_db.SaveChanges();
			return Ok(blog);
		}
		[HttpDelete("DeleteBlog")]
		public async Task<IActionResult> DeleteBlog(int id)
		{
			var blog = await _db.Blogs.FirstOrDefaultAsync(b => b.Id == id);
			if(blog is null)
			{ 
				return NotFound(); 
			}
			_db.Blogs.Remove(blog);
			_db.SaveChanges();
			return Ok(blog);
		}
	}
}
