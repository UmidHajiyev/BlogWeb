using Blog.Models;
using Blog.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Repository
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        private readonly ApplicationDbContext _db;

        public BlogPostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BlogPost post)
        {
            if (post.ImgUrl!=null)
            {
                _db.BlogPosts.Update(post);
            }
            else
            {
                BlogPost postFromDb = _db.BlogPosts.FirstOrDefault(u=>u.Id==post.Id);
                postFromDb.Title = post.Title;
                postFromDb.Description = post.Description;
                postFromDb.CategoryId = post.CategoryId;
            }
        }
    }
}
