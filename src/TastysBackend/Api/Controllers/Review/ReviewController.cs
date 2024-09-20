using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Tastys.BLL;
using Tastys.BLL.Services.Review;
using Tastys.API.Middlewares;
using DTO;

namespace Tastys.API.Controllers.Review
{

    [ApiController]
    [Route("review")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ReviewCRUD _reviewService;
        public ReviewController(ILogger<ReviewController> logger, ReviewCRUD recetaService)
        {
            _logger = logger;
            _reviewService = recetaService;
        }
        [HttpPost]
        [CheckToken]
        [CheckPermissions("user:user")]
        [SwaggerRequestExample(typeof(CreateReviewDTO), typeof(ReviewRequestExample))]
        public async Task<IActionResult> PostReview([FromBody] CreateReviewDTO reviewDTO)
        {
            try
            {
                if (HttpContext.Items["userdata"] is not UserDataToken userData)
                {
                    return BadRequest("No se encontró información del usuario.");
                }

                ReviewDto review = await _reviewService.AddReview(reviewDTO,userData.authId);

                return Ok(review);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllReview()
        {

            // Los queryParameters se validan automáticamente
            // de acuerdo a las anotaciones en RecetasQuery

            try
            {
                var review = await _reviewService.GetAllReview();

                return Ok(review);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet(":id")]
        public async Task<ActionResult> GetReviewFromrecetas(int RecetaId)
        {
            try
            {

                List<ReviewDto> review = await _reviewService.GetReviewFromRecetaId(RecetaId);

                return Ok(review);

            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public ActionResult DeleteReview([FromQuery] int id)
        {
            try
            {
                ReviewDto reviewDelete = _reviewService.IsDeleteReview(id);

                return Ok(reviewDelete);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPut]
        public ActionResult PutReview([FromBody] Domain.Review review)
        {
            try
            {
                ReviewDto reviewBuscar = _reviewService.PutReview(review);

                return Ok(reviewBuscar);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
