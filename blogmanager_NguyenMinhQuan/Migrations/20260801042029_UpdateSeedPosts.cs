using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogmanager_NguyenMinhQuan.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Author",
                value: "Nguyễn Trung");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Author",
                value: "Hồng Trung Việt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Author",
                value: "Nguyễn Minh Quân");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Author",
                value: "Admin");
        }
    }
}
