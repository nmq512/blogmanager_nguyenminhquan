using blogmanager_NguyenMinhQuan.Models;

public class CategoryListViewModel
{
    public List<Category> Categories { get; set; } = new();

    public string? Search { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalCategories { get; set; }

    public int TotalPages { get; set; }
}