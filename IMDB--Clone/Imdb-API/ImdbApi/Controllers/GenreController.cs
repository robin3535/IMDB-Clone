using System;
using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImdbApi.Controllers
{
    [Route("genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet("")]
        public IActionResult GetGenres()
        {
            return Ok(_genreService.Get());
        }
        [HttpGet("{id}")]
        public IActionResult GetGenreById([FromRoute] int id)
        {
            try
            {
                return Ok(_genreService.Get(id));
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("")]
        public IActionResult AddGenre([FromBody] GenreRequest genreRq)
        {
            try
            {
                var id = _genreService.Create(genreRq);
                return CreatedAtAction(nameof(GetGenreById), new { Id = id}, new { Id = id });
            }
            catch (FieldValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromBody] GenreRequest genreRq,[FromRoute]int id)
        {
            try
            {
                _genreService.Update(genreRq,id);
                return Ok("Updated Succesfully.");
            }
            catch (FieldValueNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }   
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre([FromRoute] int id)
        {
            try
            {
                _genreService.Delete(id);
                return Ok("Deleted Successfully.");
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
