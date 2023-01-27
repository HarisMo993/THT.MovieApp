using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Data.Context;
using THT.MovieApp.Data.Interfaces;
using THT.MovieApp.Data.Interfaces.Generic;
using THT.MovieApp.Data.Services;
using THT.MovieApp.Data.Services.Generic;

namespace THT.MovieApp.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection ConfigureIoCService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            return services;
        }
    }
}
