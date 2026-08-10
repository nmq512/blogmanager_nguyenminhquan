using blogmanager_NguyenMinhQuan.Models;
namespace blogmanager_NguyenMinhQuan.ViewModels;
public class PostListViewModel
{
    public List<Post> Posts { get; set; }
    public int? CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalPosts { get; set; }
    public string? Search { get; set; }
    public string? Sort { get; set; }
}