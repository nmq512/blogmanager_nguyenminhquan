using blogmanager_NguyenMinhQuan.Models;
using blogmanager_NguyenMinhQuan.ViewModels;
using Blogmanager_NguyenMinhQuan.Data;
using Microsoft.AspNetCore.Authorization; // Bắt buộc để dùng phân quyền
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// Mức 1 & 2: [Authorize] mặc định yêu cầu phải đăng nhập (dành cho User và Admin thực hiện Thêm/Sửa)
[Authorize]
public class PostsController : Controller
{
    private readonly ApplicationDbContext _context;
    public PostsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Mức 1: Khách vãng lai (chưa đăng nhập) được phép xem danh sách và chi tiết
    [AllowAnonymous]
    public async Task<IActionResult> Index(
        string? search,
        int? categoryId,
        int? tagId,
        int page = 1,
        string? sort = null)
    {
        int pageSize = 5;

        var query = _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Title.Contains(search));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (tagId.HasValue)
        {
            query = query.Where(p =>
                p.Tags.Any(t => t.Id == tagId.Value));
        }

        query = sort switch
        {
            "title" => query.OrderBy(p => p.Title),
            "oldest" => query.OrderBy(p => p.PublishedAt),
            _ => query.OrderByDescending(p => p.PublishedAt)
        };

        int totalPosts = await query.CountAsync();

        var posts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var categories = await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();

        var tags = await _context.Tags
            .OrderBy(t => t.Name)
            .ToListAsync();

        PostListViewModel model = new PostListViewModel
        {
            Posts = posts,
            Search = search,
            Sort = sort,
            CategoryId = categoryId,
            TagId = tagId,
            Categories = categories,
            Tags = tags,
            CurrentPage = page,
            PageSize = pageSize,
            TotalPosts = totalPosts,
            TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize)
        };

        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        return View(post);
    }

    // Mức 2 & 3: User và Admin có quyền Thêm bài viết (do có [Authorize] ở Controller)
    public async Task<IActionResult> Create()
    {
        var model = new PostCreateViewModel
        {
            Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync(),

            Tags = await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            model.Tags = await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();

            return View(model);
        }

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == model.CategoryId);

        if (!categoryExists)
        {
            ModelState.AddModelError(
                "CategoryId",
                "Danh mục được chọn không tồn tại."
            );

            model.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            model.Tags = await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();

            return View(model);
        }

        var post = new Post
        {
            Title = model.Title,
            Content = model.Content,
            PublishedAt = model.PublishedAt,
            IsPublished = model.IsPublished,
            Author = model.Author,
            ViewCount = model.ViewCount,
            CategoryId = model.CategoryId
        };

        if (model.TagIds != null && model.TagIds.Any())
        {
            post.Tags = await _context.Tags
                .Where(t => model.TagIds.Contains(t.Id))
                .ToListAsync();
        }

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Mức 2 & 3: User và Admin có quyền Sửa bài viết
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        var model = new PostEditViewModel
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            PublishedAt = post.PublishedAt,
            IsPublished = post.IsPublished,
            Author = post.Author,
            ViewCount = post.ViewCount,
            CategoryId = post.CategoryId,
            TagIds = post.Tags.Select(t => t.Id).ToList(),

            Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync(),

            Tags = await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PostEditViewModel model)
    {
        if (id != model.Id)
            return NotFound();

        if (!ModelState.IsValid)
        {
            model.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            model.Tags = await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();

            return View(model);
        }

        var post = await _context.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == model.CategoryId);

        if (!categoryExists)
        {
            ModelState.AddModelError(
                "CategoryId",
                "Danh mục được chọn không tồn tại."
            );

            model.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            model.Tags = await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();

            return View(model);
        }

        post.Title = model.Title;
        post.Content = model.Content;
        post.CategoryId = model.CategoryId;
        post.Author = model.Author;
        post.PublishedAt = model.PublishedAt;
        post.ViewCount = model.ViewCount;
        post.IsPublished = model.IsPublished;

        var selectedTagIds = model.TagIds ?? new List<int>();

        var selectedTags = await _context.Tags
            .Where(t => selectedTagIds.Contains(t.Id))
            .ToListAsync();

        post.Tags.Clear();

        foreach (var tag in selectedTags)
        {
            post.Tags.Add(tag);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Mức 3: CHỈ ADMIN mới có quyền Xóa bài viết
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        return View(post);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}