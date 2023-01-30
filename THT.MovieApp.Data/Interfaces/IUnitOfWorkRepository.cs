using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Data.Context;

namespace THT.MovieApp.Data.Interfaces
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        ApplicationDbContext Context { get; }
        IActorRepository Actors { get; }
        IDirectorRepository Directors { get; }
        IGenreRepository Genres { get; }
        IMovieRepository Movies { get; }
        Task Save();
    }
}
