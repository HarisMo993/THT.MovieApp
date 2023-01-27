using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Data.Context;
using THT.MovieApp.Data.Interfaces;
using THT.MovieApp.Data.Services.Generic;
using THT.MovieApp.Domain.Models;

namespace THT.MovieApp.Data.Services
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
