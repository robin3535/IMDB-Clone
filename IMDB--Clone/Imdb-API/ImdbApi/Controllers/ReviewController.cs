using System;
using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImdbApi.Controllers
{
    [Route("movies/{movieId}/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("")]
        public IActionResult GetReviewsByMovieId(int movieId)
        {
                return Ok(_reviewService.GetReviewsByMovieId(movieId));
        }
        [HttpGet("~/reviews/{id}")]
        public IActionResult GetReviewById([FromRoute] int id)
        {
            try
            {
                return Ok(_reviewService.Get(id));
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("")]
        public IActionResult AddReview([FromBody] ReviewRequest reviewRq,int movieId)
        {
            try
            {
                var id = _reviewService.Create(reviewRq,movieId);
                return CreatedAtAction(nameof(GetReviewById), new { Id = id }, new { Id = id });
            }
            catch(RecordNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidFieldValueException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("~/reviews/{id}")]
        public IActionResult UpdateReview([FromBody] ReviewRequest reviewRq,[FromRoute]int id)
        {
            try
            {
                _reviewService.Update(reviewRq,id);
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
        [HttpDelete("~/reviews/{id}")]
        public IActionResult DeleteReview([FromRoute] int id)
        {
            try
            {
                _reviewService.Delete(id);
                return Ok("Deleted Successfully.");
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
