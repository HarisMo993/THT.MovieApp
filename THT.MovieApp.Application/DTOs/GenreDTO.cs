using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THT.MovieApp.Domain.Models;
using THT.MovieApp.Domain.Validations;

namespace THT.MovieApp.Application.DTOs
{
    public class GenreCreationDTO
    {
        //[Required(ErrorMessage = "The field with name {0} is required")]
        //[StringLength(50)]
        //[FirstLetterUppercase]
        public string Name { get; set; }
    }

    public class GenreUpdateDTO : GenreCreationDTO
    {
    }

    public class GenreDTO : GenreCreationDTO
    {
        public int Id { get; set; }
    }
}
