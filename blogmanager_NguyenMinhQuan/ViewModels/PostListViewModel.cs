using blogmanager_NguyenMinhQuan.Models;

namespace blogmanager_NguyenMinhQuan.ViewModels;

public class PostListViewModel
{
    public List<Post> Posts { get; set; } = new();

    public int? CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalPosts { get; set; }

    public string? Search { get; set; }

    public string? Sort { get; set; }

    // Bộ lọc Category
    public int? CategoryId { get; set; }

    // Bộ lọc Tag
    public int? TagId { get; set; }

    // Danh sách Category để hiển thị trong bộ lọc
    public List<Category> Categories { get; set; } = new();

    // Danh sách Tag để hiển thị trong bộ lọc
    public List<Tag> Tags { get; set; } = new();
}