using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories;
using Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Data;
using Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lazulisoft.ApiCleanArchtMediatR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}