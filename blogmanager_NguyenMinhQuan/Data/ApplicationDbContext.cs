using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using blogmanager_NguyenMinhQuan.Models;

namespace Blogmanager_NguyenMinhQuan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
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
                ViewCount = 120,
                CategoryId = 1
            },
            new Post
            {
                Id = 2,
                Title = "Học Entity Framework Core",
                Content = "Làm quen với Migration.",
                Author = "Nguyễn Minh Quân",
                PublishedAt = new DateTime(2026, 8, 2),
                IsPublished = true,
                ViewCount = 250,
                CategoryId = 2
            },
            new Post
            {
                Id = 3,
                Title = "Razor View",
                Content = "Sử dụng Razor để hiển thị dữ liệu.",
                Author = "Hồng Trung Việt",
                PublishedAt = new DateTime(2026, 8, 3),
                IsPublished = false,
                ViewCount = 60,
                CategoryId = 3
            }
            );
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Công nghệ" },
                new Category { Id = 2, Name = "Lập trình" },
                new Category { Id = 3, Name = "Web Development" },
                new Category { Id = 4, Name = "Database" }
            );
            builder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "ASP.NET Core" },
                new Tag { Id = 2, Name = "Entity Framework" },
                new Tag { Id = 3, Name = "Razor" }
            );
        }
    }
}