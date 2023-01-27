using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THT.MovieApp.Data.Interfaces
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        IActorRepository ActorRepository { get; }
        IDirectorRepository DirectorRepository { get; }
        IGenreRepository GenreRepository { get; }
        IMovieRepository MovieRepository { get; }
        Task Save();
    }
}
