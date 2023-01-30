using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Data.Context;
using THT.MovieApp.Data.Interfaces;

namespace THT.MovieApp.Data.Services
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ApplicationDbContext _context;
        private IActorRepository _actorRepository;
        private IDirectorRepository _directorRepository;
        private IGenreRepository _genreRepository;
        private IMovieRepository _movieRepository;

        public UnitOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }

        public IActorRepository Actors =>
            _actorRepository ??= new ActorRepository(_context);

        public IDirectorRepository Directors =>
            _directorRepository ??= new DirectorRepository(_context);

        public IGenreRepository Genres =>
            _genreRepository ??= new GenreRepository(_context);

        public IMovieRepository Movies =>
            _movieRepository ??= new MovieRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
