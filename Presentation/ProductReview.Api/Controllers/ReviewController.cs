using Microsoft.AspNetCore.Mvc;
using Review.Persistance;

namespace ProductReview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService) =>
            _reviewService = reviewService;

        [HttpGet]
        public async Task<List<Review.Domain.Entities.Review>> Get() =>
            await _reviewService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Review.Domain.Entities.Review>> Get(string id)
        {
            var comment = await _reviewService.GetAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Review.Domain.Entities.Review newComment)
        {
            await _reviewService.CreateAsync(newComment);

            return CreatedAtAction(nameof(Get), new { id = newComment.Id }, newComment);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Review.Domain.Entities.Review updatedComment)
        {
            var comment = await _reviewService.GetAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            updatedComment.Id = comment.Id;

            await _reviewService.UpdateAsync(id, updatedComment);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var comment = await _reviewService.GetAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            await _reviewService.RemoveAsync(id);

            return NoContent();
        }
    }
}