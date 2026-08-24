using blogmanager_NguyenMinhQuan.Models;
using System.ComponentModel.DataAnnotations;

namespace blogmanager_NguyenMinhQuan.ViewModels
{
    public class PostCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(200, MinimumLength = 3,
            ErrorMessage = "Tiêu đề phải từ 3 đến 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        [Display(Name = "Ngày xuất bản")]
        [DataType(DataType.Date)]
        public DateTime PublishedAt { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; }

        public string Author { get; set; } = string.Empty;

        public int ViewCount { get; set; }

        public int CategoryId { get; set; }

        // Các Tag được chọn
        public List<int> TagIds { get; set; } = new();

        // Dùng để hiển thị danh sách Category
        public List<Category> Categories { get; set; } = new();

        // Dùng để hiển thị danh sách Tag
        public List<Tag> Tags { get; set; } = new();
    }
}