using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Author")]
        public string UserId { get; set; }
        [ValidateNever]
        public User user { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
