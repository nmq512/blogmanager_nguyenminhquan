using blogmanager_NguyenMinhQuan.Models;

public class PostListViewModel
{
    public List<Post> Posts { get; set; }
    public string? Search { get; set; }
    public int? CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalPosts { get; set; }
}