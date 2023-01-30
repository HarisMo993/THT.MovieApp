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
    public class ActorController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly ILogger<ActorController> _logger;
        private readonly IMapper _mapper;

        public ActorController(IUnitOfWorkRepository unitOfWork, ILogger<ActorController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: api/<ActorController>
        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            try
            {
                var actors = await _unitOfWork.Actors.Get();
                var results = _mapper.Map<IList<ActorDTO>>(actors);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetActors)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // GET api/<ActorController>/5
        [HttpGet("{id}", Name = "GetActor")]
        public async Task<IActionResult> GetActor(int id)
        {
            try
            {
                var actor = await _unitOfWork.Actors.GetById(q => q.Id == id);
                var result = _mapper.Map<ActorDTO>(actor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetActors)}");
                return StatusCode(500, "Interval Server Error. Please Try Again Later.");
            }
        }

        // POST api/<ActorController>
        [HttpPost]
        public async Task<IActionResult> CreateActor([FromBody] ActorCreationDTO actorDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateActor)}");
                return BadRequest(ModelState);
            }

            try
            {
                var actor = _mapper.Map<Actor>(actorDTO);

                await _unitOfWork.Actors.Add(actor);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetActor", new { id = actor.Id }, actor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateActor)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT api/<ActorController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] ActorUpdateDTO actorDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateActor)}");
                return BadRequest(ModelState);
            }

            try
            {
                var actor = await _unitOfWork.Actors.GetById(q => q.Id == id);

                if (actor == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateActor)}");
                    return BadRequest(ModelState);
                }

                _mapper.Map(actorDTO, actor);
                _unitOfWork.Actors.Update(actor);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateActor)}");
                return StatusCode(500, "Interval server error. Please try again later.");
            }
        }

        // DELETE api/<ActorController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteActor)}");
                return BadRequest();
            }

            try
            {
                var actor = await _unitOfWork.Actors.GetById(q => q.Id == id);
                if (actor == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteActor)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Actors.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteActor)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }
        }
    }
}
