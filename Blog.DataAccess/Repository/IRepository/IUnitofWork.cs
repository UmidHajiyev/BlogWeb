using Blog.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
        ICommentRepository Comment { get; }
        IBlogPostRepository Post { get; }
        IUserRepository User { get; }
        public void Save();
    }
}
