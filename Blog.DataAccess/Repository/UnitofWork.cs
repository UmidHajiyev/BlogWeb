using Blog.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;
        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Post = new BlogPostRepository(_db);
            User = new UserRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }
        public IBlogPostRepository Post { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
