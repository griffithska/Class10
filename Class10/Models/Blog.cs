using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Class10.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
