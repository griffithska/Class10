using System.ComponentModel.DataAnnotations;

namespace Class10.Models
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
