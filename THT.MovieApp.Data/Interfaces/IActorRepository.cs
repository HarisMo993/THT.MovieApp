using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Data.Interfaces.Generic;
using THT.MovieApp.Domain.Models;

namespace THT.MovieApp.Data.Interfaces
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
    }
}
