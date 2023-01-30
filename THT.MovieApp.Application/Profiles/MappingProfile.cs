using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Application.DTOs;
using THT.MovieApp.Domain.Models;

namespace THT.MovieApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Genre Mapping
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>().ReverseMap();
            #endregion Genre

            #region Actor Mapping
            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>().ReverseMap();
            #endregion Actor

            #region Director Mapping
            CreateMap<DirectorDTO, Director>().ReverseMap();
            CreateMap<DirectorCreationDTO, Director>().ReverseMap();
            #endregion Director

            #region Movie Mapping
            CreateMap<MovieCreationDTO, Movie>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            #endregion Movie
        }

    }
}
