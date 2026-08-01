using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using blogmanager_NguyenMinhQuan.Models;

namespace Blogmanager_NguyenMinhQuan.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Giới thiệu ASP.NET Core",
                    Content = "Đây là bài viết đầu tiên.",
                    Author = "Nguyễn Trung",
                    PublishedAt = new DateTime(2026, 8, 1),
                    IsPublished = true,
                    ViewCount = 120
                },
                new Post
                {
                    Id = 2,
                    Title = "Học Entity Framework Core",
                    Content = "Làm quen với Migration.",
                    Author = "Nguyễn Minh Quân",
                    PublishedAt = new DateTime(2026, 8, 2),
                    IsPublished = true,
                    ViewCount = 250
                },
                new Post
                {
                    Id = 3,
                    Title = "Razor View",
                    Content = "Sử dụng Razor để hiển thị dữ liệu.",
                    Author = "Hồng Trung Việt",
                    PublishedAt = new DateTime(2026, 8, 3),
                    IsPublished = false,
                    ViewCount = 60
                }
            );
        }
    }
}