using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blogmanager_NguyenMinhQuan.Data;
using blogmanager_NguyenMinhQuan.Models;
using blogmanager_NguyenMinhQuan.Dtos;

namespace blogmanager_NguyenMinhQuan.Controllers.Api
{
    [ApiController]
    [Route("api/posts")]
    public class PostsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/posts - Lấy danh sách bài viết (Mã 200 OK)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            var posts = await _context.Posts
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Author = p.Author,
                    PublishedAt = p.PublishedAt,
                    IsPublished = p.IsPublished,
                    ViewCount = p.ViewCount,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();

            return Ok(posts);
        }

        // 2. GET: api/posts/{id} - Lấy chi tiết một bài viết theo ID (Mã 200 OK hoặc 404 Not Found)
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var p = await _context.Posts.FindAsync(id);

            if (p == null)
            {
                return NotFound(); // 404
            }

            var postDto = new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Author = p.Author,
                PublishedAt = p.PublishedAt,
                IsPublished = p.IsPublished,
                ViewCount = p.ViewCount,
                CategoryId = p.CategoryId
            };

            return Ok(postDto); // 200
        }

        // 3. POST: api/posts - Tạo mới bài viết (Mã 201 Created hoặc 400 Bad Request)
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost(PostCreateDto dto)
        {
            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                PublishedAt = dto.PublishedAt,
                IsPublished = dto.IsPublished,
                ViewCount = dto.ViewCount,
                CategoryId = dto.CategoryId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            var result = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
                PublishedAt = post.PublishedAt,
                IsPublished = post.IsPublished,
                ViewCount = post.ViewCount,
                CategoryId = post.CategoryId
            };

            // Trả về 201 kèm header Location trỏ đến hàm GetPost
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, result);
        }

        // 4. PUT: api/posts/{id} - Cập nhật bài viết (Mã 204 NoContent, 400 Bad Request hoặc 404 Not Found)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostCreateDto dto)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound(); // 404
            }

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.Author = dto.Author;
            post.PublishedAt = dto.PublishedAt;
            post.IsPublished = dto.IsPublished;
            post.ViewCount = dto.ViewCount;
            post.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        // 5. DELETE: api/posts/{id} - Xóa bài viết (Mã 204 NoContent hoặc 404 Not Found)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound(); // 404
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }
    }
}