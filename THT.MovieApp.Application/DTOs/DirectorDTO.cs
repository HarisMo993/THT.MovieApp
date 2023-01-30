using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THT.MovieApp.Application.DTOs
{
    public class DirectorCreationDTO
    {
        //[Required]
        //[StringLength(120)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
    }

    public class DirectorUpdateDTO : DirectorCreationDTO
    {
        public IList<MovieDTO> Movies { get; set; }
    }

    public class DirectorDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public List<MovieDTO> Movies { get; set; }
    }
}
