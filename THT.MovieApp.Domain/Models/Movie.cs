using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THT.MovieApp.Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 75)]
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public double Budget { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public IList<MoviesGenres> MoviesGenres { get; set; }
        public IList<MoviesActors> MoviesActors { get; set; }
    }
}
