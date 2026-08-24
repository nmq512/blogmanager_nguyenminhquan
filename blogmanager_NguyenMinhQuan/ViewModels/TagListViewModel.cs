using blogmanager_NguyenMinhQuan.Models;

public class TagListViewModel
{
    public List<Tag> Tags { get; set; } = new();

    public string? Search { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalTags { get; set; }

    public int TotalPages { get; set; }
}

