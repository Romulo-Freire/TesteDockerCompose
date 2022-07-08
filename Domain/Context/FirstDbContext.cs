using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Context
{
    public class FirstDbContext : DbContext
    {
        public FirstDbContext(DbContextOptions<FirstDbContext> options) : base(options)
        {
           
        }

        public DbSet<UserEntity> User { get; set; }
    }
}
