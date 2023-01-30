using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using THT.MovieApp.Application.DTOs;
using THT.MovieApp.Data.Interfaces;
using THT.MovieApp.Domain.Helpers;
using THT.MovieApp.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace THT.MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly ILogger<DirectorController> _logger;
        private readonly IMapper _mapper;

        public DirectorController(IUnitOfWorkRepository unitOfWork, ILogger<DirectorController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<DirectorController>
        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            try
            {
                var directors = await _unitOfWork.Directors.Get();
                var results = _mapper.Map<IList<DirectorDTO>>(directors);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDirectors)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // GET api/<DirectorController>/5
        [HttpGet("{id:int}", Name = "GetDirector")]
        public async Task<IActionResult> GetDirector(int id)
        {
            try
            {
                var director = await _unitOfWork.Directors.GetById(q => q.Id == id, new List<string> { "Movies" });
                var result = _mapper.Map<DirectorDTO>(director);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDirectors)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // POST api/<DirectorController>
        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromBody] DirectorCreationDTO directorDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateDirector)}");
                return BadRequest(ModelState);
            }

            try
            {
                var director = _mapper.Map<Director>(directorDTO);

                await _unitOfWork.Directors.Add(director);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetDirector", new { id = director.Id }, director);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateDirector)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT api/<DirectorController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDirector(int id, [FromBody] DirectorUpdateDTO directorDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateDirector)}");
                return BadRequest(ModelState);
            }

            try
            {
                var director = await _unitOfWork.Directors.GetById(q => q.Id == id, new List<string> { "Movies" });
                
                if (director == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateDirector)}");
                    return BadRequest(ModelState);
                }

                _mapper.Map(directorDTO, director);

                _unitOfWork.Directors.Update(director);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateDirector)}");
                return StatusCode(500, "Interval server error. Please try again later.");
            }
        }

        // DELETE api/<DirectorController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteDirector)}");
                return BadRequest();
            }

            try
            {
                var director = await _unitOfWork.Directors.GetById(q => q.Id == id);
                if (director == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteDirector)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Directors.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteDirector)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
