using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Domain.Helpers;
using THT.MovieApp.Domain.Models;

namespace THT.MovieApp.Application.DTOs
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public DateTime ReleaseDate { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<MoviesActorsCreationDTO>>))]
        public List<MoviesActorsCreationDTO> Actors { get; set; }
        public int DirectorId { get; set; }
    }

    public class MovieUpdateDTO : MovieCreationDTO
    {
        public IList<ActorDTO> ActorsDTO { get; set; }
        public IList<Genre> GenresDTO { get; set; }
    }

    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IList<ActorDTO> ActorsDTO { get; set; }
        public IList<Genre> GenresDTO { get; set; }
        public DirectorDTO Director { get; set; }
    }
}
