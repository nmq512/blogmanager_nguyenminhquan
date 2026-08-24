namespace blogmanager_NguyenMinhQuan.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public int ViewCount { get; set; }
        public int CategoryId { get; set; }
    }
}