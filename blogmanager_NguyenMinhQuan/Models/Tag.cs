using System.ComponentModel.DataAnnotations;
using blogmanager_NguyenMinhQuan.Models;

public class Tag
{
    public int Id { get; set; }
        
    [Required(ErrorMessage = "Tên tag không được để trống")]
    [Display(Name = "Tên tag")]
    public string Name { get; set; }=string.Empty;
    public List<Post> Posts { get; set; } = new();
}