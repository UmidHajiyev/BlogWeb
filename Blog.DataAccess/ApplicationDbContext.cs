using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                           .Ignore(c => c.LockoutEnabled)
                                           .Ignore(c => c.LockoutEnd)
                                           .Ignore(c => c.TwoFactorEnabled)
                                           .Ignore(c => c.PhoneNumber)
                                           .Ignore(c => c.PhoneNumberConfirmed)
                                           .Ignore(c => c.EmailConfirmed);
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
