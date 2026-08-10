using blogmanager_NguyenMinhQuan.Models;
using blogmanager_NguyenMinhQuan.ViewModels;
using Blogmanager_NguyenMinhQuan.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class PostsController : Controller
{
    private readonly ApplicationDbContext _context;
    public PostsController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(string? search, int page = 1, string? sort = null)
{
    int pageSize = 5;

    var query = _context.Posts.Include(p => p.Category).Include(p => p.Category).AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(p => p.Title.Contains(search));
    }
    query= sort switch
    {
        "title" => query.OrderBy(p => p.Title),
        "oldest" => query.OrderBy(p => p.PublishedAt),
        _ => query.OrderByDescending(p => p.PublishedAt),
    };

    int totalPosts = await query.CountAsync();

    var posts = await query
        .Include(p => p.Tags)
        .Include(p => p.Category)
        .OrderBy(p => p.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    PostListViewModel model = new PostListViewModel
    {
        Posts = posts,
        Search = search,
        Sort = sort,
        CurrentPage = page,
        PageSize = pageSize,
        TotalPosts = totalPosts,
        TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize)
    };

    return View(model);
}
    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.Posts
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound();

        return View(post);
    }
    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(post);
        }

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == post.CategoryId);

        if (!categoryExists)
        {
            ModelState.AddModelError(
                "CategoryId",
                "Danh mục được chọn không tồn tại."
            );

        ViewBag.Categories = _context.Categories.ToList();

        return View(post);
        }

    _context.Posts.Add(post);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
            return NotFound();

        ViewBag.Categories = _context.Categories.ToList();

        return View(post);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Post post)
    {
        if (id != post.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(post);
        } 

        _context.Update(post);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
            return NotFound();

        return View(post);
    }
    [HttpPost]
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