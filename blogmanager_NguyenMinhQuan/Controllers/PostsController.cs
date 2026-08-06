using blogmanager_NguyenMinhQuan.Models;
using Blogmanager_NguyenMinhQuan.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PostsController : Controller
{
    private readonly ApplicationDbContext _context;
    public PostsController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(string? search, int page = 1)
{
    int pageSize = 5;

    var query = _context.Posts.AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(p => p.Title.Contains(search));
    }

    int totalPosts = await query.CountAsync();

    var posts = await query
        .OrderBy(p => p.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    PostListViewModel model = new PostListViewModel
    {
        Posts = posts,
        Search = search,
        CurrentPage = page,
        PageSize = pageSize,
        TotalPosts = totalPosts,
        TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize)
    };

    return View(model);
}
    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();
        return View(post);
    }
    public IActionResult Create()=> View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        if (!ModelState.IsValid)
            return View(post);

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();
        return View(post);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Post post)
    {
        if (id != post.Id) return NotFound();
        if (!ModelState.IsValid) return View(post);

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