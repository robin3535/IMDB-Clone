using System;
using System.Reflection.Metadata.Ecma335;
using Firebase.Storage;
using System.Threading.Tasks;
using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ImdbApi.Common;

namespace ImdbApi.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;

        public MovieController(IMovieService movieService, IActorService actorService)
        {
            _movieService = movieService;
            _actorService = actorService;
        }
        [HttpGet("")]
        public IActionResult GetMovies([FromQuery] MovieQueryFilter filter)
        {
            return Ok(_movieService.Get(filter));

        }
        [HttpGet("{id}")]
        public IActionResult GetMovieById([FromRoute] int id)
        {
            try
            {
                return Ok(_movieService.Get(id));
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("{movieId}/actors")]
        public IActionResult GetActorsByMovieId([FromRoute] int movieId)
        {
            try
            {
                return Ok(_actorService.GetActorByMovieId(movieId));
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("")]
        public IActionResult AddMovie([FromBody] MovieRequest rmovie)
        {
            try
            {
                var id = _movieService.Create(rmovie);
                return CreatedAtAction(nameof(GetMovieById), new { Id = id }, new { Id = id });
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
        [HttpPut("{id}")]
        public IActionResult UpdateMovie([FromBody] MovieRequest movie, [FromRoute] int id)
        {
            try
            {
                _movieService.Update(movie, id);
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
        [HttpPost("upload/{name}")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromRoute] string name)
        {
            DateTime date = new DateTime();
            var fileName = name + "/" + date.Day + "-" + date.Month + "-" + date.Year + "-" + date.Second + "-img";
            if (file == null || file.Length == 0)
                return Content("file not selected");
            var task = await new FirebaseStorage("bootcamp-d5ab1.appspot.com")
                    .Child("CoverImage")
                    .Child(fileName + ".jpg")
                    .PutAsync(file.OpenReadStream());
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie([FromRoute] int id)
        {
            try
            {
                _movieService.Delete(id);
                return Ok("Deleted Successfully.");
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        // [HttpGet("Search")]
        // public IActionResult SearchMovie([FromQuery] string name, [FromQuery] string genre)
        // {
        //     var result = _movieService.Get();
        //     if(String.IsNullOrEmpty(name) && String.IsNullOrEmpty(genre))
        //     {
        //         return BadRequest("Please provide search value");
        //     }
        //     if (!String.IsNullOrEmpty(name))
        //     {
        //         result = result.Where(m => m.Name == name); 
        //     }
        //     if (!String.IsNullOrEmpty(genre))
        //     {
        //         result = result.Where(m => m.Name == name);
        //     }
        //     return Ok(result);
        // }
    }
}
