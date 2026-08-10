using System.ComponentModel.DataAnnotations;

namespace blogmanager_NguyenMinhQuan.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Tiêu đề phải từ 3 đến 200 ký tự")]
        public string Title { get; set; }=string.Empty;
        public string Content { get; set; }=string.Empty;
        
        [Display(Name = "Ngày xuất bản")]
        [DataType(DataType.Date)]
        public DateTime PublishedAt { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; }
        public string Author { get; set; }=string.Empty;
        public int ViewCount { get; set; }
        public string NhanPhoBien => ViewCount >= 100 ? "Phổ biến" : "Thường";
        public string MoTaNgan() => $"{Title} ({PublishedAt:dd/MM/yyyy})";
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Tag> Tags { get; set; } = new();
    }
}