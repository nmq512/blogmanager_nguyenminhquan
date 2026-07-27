using blogmanager_NguyenMinhQuan.Models;
using Microsoft.AspNetCore.Mvc;

public class LabController : Controller
{
    public IActionResult Index()
    {
        var baiViet = new List<Post>
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
        ViewBag.SoDaXuatBan = baiViet.Count(p => p.IsPublished);
        ViewBag.BaiViet = baiViet.Where(p => p.IsPublished).OrderBy(p => p.Title).ToList();
        return View();
    }
    public IActionResult CountView()
    {
        var baiViet = new List<Post>
        {
            new Post
            {
                Id = 1, Title = "C# cơ bản", IsPublished = true, ViewCount=10, Author = "Nguyễn Phương"
            },
            new Post
            {
                Id = 2, Title = "MVC nhập môn", IsPublished = false, ViewCount=50, Author = "Nguyễn Trung"
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
                Id = 5, Title = "ASP.NET Core", IsPublished = true, ViewCount=120, Author = "Nguyễn Minh Quân"
            }
        };
        ViewBag.TongLuotXem = baiViet.Sum(p => p.ViewCount);
        ViewBag.BaiViet = baiViet.Where(p => p.IsPublished).OrderBy(p => p.ViewCount).ToList();
        var baiVietNhieuViewNhat = baiViet.OrderByDescending(p => p.ViewCount).First();
        ViewBag.BaiVietNhieuViewNhat = baiVietNhieuViewNhat;  
        return View();
    }
}