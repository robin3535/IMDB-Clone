using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ImdbApi.Controllers
{
    [Route("actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService; 
        public ActorController(IActorService actorService) {

            _actorService = actorService;
        }
        [HttpGet("")]
        public IActionResult GetActors()
        {
               return Ok(_actorService.Get());
      
        }
        [HttpGet("{id}")]
        public IActionResult GetActorById([FromRoute]int id) {

            try
            {
                return Ok(_actorService.Get(id));
            }
            catch(RecordNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
           
        
        }
        [HttpPost("")]
        public IActionResult AddActor([FromBody]ActorRequest actorRq)
        {
            try
            {
                var id = _actorService.Create(actorRq);
                return CreatedAtAction(nameof(GetActorById), new { Id = id}, new { Id = id });
            }
            catch (FieldValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidFieldValueException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody]ActorRequest actorRq,[FromRoute]int id)
        {
            try
            {
                _actorService.Update(actorRq,id);
                return Ok("Updated Succesfully.");
            }
            catch (FieldValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidFieldValueException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPatch("{id}")]
        public IActionResult UpdatePatch([FromBody] JsonPatchDocument actorPatch, [FromRoute]int id)
        {
            try
            {
                _actorService.UpdatePatch(actorPatch, id);
                return Ok("Updated Succesfully.");
            }
            catch (FieldValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidFieldValueException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteActor([FromRoute]int id)
        {
            try
            {
                _actorService.Delete(id);
                return Ok("Deleted Successfully.");
            }
            catch(RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
