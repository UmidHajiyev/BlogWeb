using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.ViewModel
{
    public class PostDetailViewModel
    {
        public BlogPost post { get; set; }
        public IEnumerable<Comment> comments { get; set; }
    }
}
