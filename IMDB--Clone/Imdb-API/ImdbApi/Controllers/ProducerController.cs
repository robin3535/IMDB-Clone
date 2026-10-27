using System;
using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImdbApi.Controllers
{
    [Route("producers")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _producerService;
        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        [HttpGet("")]
        public IActionResult GetProducers()
        {
                return Ok(_producerService.Get());
        }
        [HttpGet("{id}")]
        public IActionResult GetProducerById([FromRoute] int id)
        {
            try
            {
                return Ok(_producerService.Get(id));
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
        [HttpPost("")]
        public IActionResult AddProducer([FromBody] ProducerRequest producerRq)
        {
            try
            {
                var id = _producerService.Create(producerRq);
                return CreatedAtAction(nameof(GetProducerById), new { Id = id }, new { Id = id });
            }
            catch (FieldValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidFieldValueException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProducer([FromBody] ProducerRequest producerRq,[FromRoute]int id)
        {
            try
            {
                _producerService.Update(producerRq, id);
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
        public IActionResult DeleteProducer([FromRoute] int id)
        {
            try
            {
                _producerService.Delete(id);
                return Ok("Deleted Successfully.");
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
