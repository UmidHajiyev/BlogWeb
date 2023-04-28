using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int postId { get; set; }
        [Required]
        public string userId { get; set; }
        [ValidateNever]
        public User user { get; set; }
        [Required]
        public string content { get; set; }
    }
}
