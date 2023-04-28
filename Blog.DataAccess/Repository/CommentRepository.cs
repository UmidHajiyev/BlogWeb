using Blog.DataAccess.Repository.IRepository;
using Blog.Models;
using Blog.DataAccess;
using Blog.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Comment comment)
        {
            _db.Update(comment);
        }
    }
}
