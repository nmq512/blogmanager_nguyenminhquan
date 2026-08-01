using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blogmanager_NguyenMinhQuan.Migrations
{
    /// <inheritdoc />
    public partial class SeedPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Content", "IsPublished", "PublishedAt", "Title", "ViewCount" },
                values: new object[,]
                {
                    { 1, "Nguyễn Minh Quân", "Đây là bài viết đầu tiên.", true, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giới thiệu ASP.NET Core", 120 },
                    { 2, "Nguyễn Minh Quân", "Làm quen với Migration.", true, new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Học Entity Framework Core", 250 },
                    { 3, "Admin", "Sử dụng Razor để hiển thị dữ liệu.", false, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Razor View", 60 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
