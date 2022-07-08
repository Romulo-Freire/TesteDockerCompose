using Domain.Context;
using Domain.DomainUser;
using Domain.RepositoryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class Initialize
    {
        public static void Init(IServiceCollection services)
        {
            services.AddDbContext<FirstDbContext>(options => {
                options.UseSqlServer("Server=host.docker.internal,1433;Database=testedocker;User ID=sa;Password=Rm17Op12;", x => x.MigrationsAssembly("Domain"));
            });

            services.AddScoped<FirstDbContext>();

            services.AddScoped<IDomainUser, DomainUser>();

            services.AddScoped<IRepository<UserEntity>, Repository<UserEntity>>();
        }
    }
}
