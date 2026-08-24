
using blogmanager_NguyenMinhQuan.Models;
using Blogmanager_NguyenMinhQuan.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogmanager_NguyenMinhQuan.Controllers
{
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            int pageSize = 5;

            var query = _context.Tags.AsQueryable();

            // Tìm kiếm tag theo tên
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }

            // Tổng số tag
            int totalTags = await query.CountAsync();

            // Lấy danh sách tag theo trang
            var tags = await query
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            TagListViewModel model = new TagListViewModel
            {
                Tags = tags,
                Search = search,
                CurrentPage = page,
                PageSize = pageSize,
                TotalTags = totalTags,
                TotalPages = (int)Math.Ceiling(
                    (double)totalTags / pageSize
                )
            };

            return View(model);
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
                return View(tag);

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // POST: Tags/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tag tag)
        {
            if (id != tag.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(tag);

            _context.Update(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

