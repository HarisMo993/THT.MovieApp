using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THT.MovieApp.Application.DTOs;
using THT.MovieApp.Data.Interfaces;
using THT.MovieApp.Domain.Helpers;
using THT.MovieApp.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace THT.MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWorkRepository unitOfWork, ILogger<MovieController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var movies = await _unitOfWork.Movies.Get();
                var results = _mapper.Map<IList<MovieDTO>>(movies);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMovies)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // GET api/<MovieController>/5
        [HttpGet("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetById(q => q.Id == id, new List<string> { "ActorsDTO", "GenresDTO", "Director" });
                var result = _mapper.Map<MovieDTO>(movie);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetMovies)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreationDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateMovie)}");
                return BadRequest(ModelState);
            }

            try
            {
                var movie = _mapper.Map<Movie>(movieDTO);

                AnnotateActorsOrder(movie);
                await _unitOfWork.Movies.Add(movie);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetMovie", new { id = movie.Id }, movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateMovie)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

      

        // PUT api/<MovieController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDTO movieDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMovie)}");
                return BadRequest(ModelState);
            }

            try
            {
                var movie = await _unitOfWork.Movies.GetById(q => q.Id == id, new List<string> { "ActorsDTO", "GenresDTO", "Director" });

                if (movie == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateMovie)}");
                    return BadRequest(ModelState);
                }

                _mapper.Map(movieDTO, movie);

                AnnotateActorsOrder(movie);
                _unitOfWork.Movies.Update(movie);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateMovie)}");
                return StatusCode(500, "Interval server error. Please try again later.");
            }
        }

        private void AnnotateActorsOrder(Movie movie)
        {
            if (movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMovie)}");
                return BadRequest();
            }

            try
            {
                var movie = await _unitOfWork.Movies.GetById(q => q.Id == id);
                if (movie == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteMovie)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Movies.Delete(id);
                await _unitOfWork.Save();


                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteMovie)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
