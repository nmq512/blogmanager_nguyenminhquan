using blogmanager_NguyenMinhQuan.Models;
using Microsoft.AspNetCore.Mvc;

public class PostsController : Controller
{
    List<Post> getListPost()
    {
         var posts = new List<Post>
        {
            new Post
            {
                Id = 1, Title = "C# cơ bản", IsPublished = true, ViewCount=1000, Author = "Nguyễn Phương"
            },
            new Post
            {
                Id = 2, Title = "MVC nhập môn", IsPublished = false, ViewCount=500, Author = "Nguyễn Trung"
            },
            new Post
            {
                Id = 3, Title = "EF Core", IsPublished = true, ViewCount=75, Author = "Đặng Anh Quân"
            },
            new Post
            {
                Id = 4, Title = "Blazor", IsPublished = false, ViewCount=30, Author = "Hồng Trung Việt"
            },
            new Post
            {
                Id = 5, Title = "ASP.NET Core", IsPublished = true, ViewCount=12, Author = "Nguyễn Minh Quân"
            }
        };
        return posts;
    }
    public IActionResult Index()
    {
        var posts = getListPost();
        ViewData["Title"] = "Danh sách bài viết";
        ViewBag.PublishedCount = posts.Count(p => p.IsPublished);
        return View(posts);
    }
    public IActionResult Details(int id)
    {
        var posts = getListPost();
        var post = posts.Where(p => p.Id==id).FirstOrDefault();
        if (post != null)
            return View(post);
        else
            return NotFound();
    }
}