using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using THT.MovieApp.Application.DTOs;
using THT.MovieApp.Data.Interfaces;
using THT.MovieApp.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace THT.MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;

        public GenreController(IUnitOfWorkRepository unitOfWork, ILogger<GenreController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            try
            {
                var genres = await _unitOfWork.Genres.Get();
                var results = _mapper.Map<IList<GenreDTO>>(genres);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetGenres)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // GET api/<GenreController>/5
        [HttpGet("{id:int}", Name = "GetGenre")]
        public async Task<IActionResult> GetGenre(int id)
        {
            try
            {
                var genre = await _unitOfWork.Genres.GetById(q => q.Id == id);
                var result = _mapper.Map<GenreDTO>(genre);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetGenres)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreationDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateGenre)}");
                return BadRequest(ModelState);
            }

            try
            {
                var genre = _mapper.Map<Genre>(genreDTO);
                await _unitOfWork.Genres.Add(genre);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetGenre", new { id = genre.Id }, genre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateGenre)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDTO genreDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateGenre)}");
                return BadRequest(ModelState);
            }

            try
            {
                var genre = await _unitOfWork.Genres.GetById(q => q.Id == id);
                if (genre == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateGenre)}");
                    return BadRequest(ModelState);
                }

                _mapper.Map(genreDTO, genre);
                _unitOfWork.Genres.Update(genre);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateGenre)}");
                return StatusCode(500, "Interval server error. Please try again later.");
            }
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteGenre)}");
                return BadRequest();
            }

            try
            {
                var genre = await _unitOfWork.Genres.GetById(q => q.Id == id);
                if (genre == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteGenre)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Genres.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteGenre)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
