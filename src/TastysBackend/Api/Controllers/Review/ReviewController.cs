using Microsoft.AspNetCore.Mvc;
using Tastys.BLL;
using Tastys.BLL.Services.Review;


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
        public async Task<IActionResult> PostReview([FromBody]Domain.Review reviewDTO)
        {
            try
            {
                ReviewDto review =  await _reviewService.AddReview(reviewDTO);

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

        [HttpGet("{Id}")]
        public ActionResult GetReview( int Id)
        {
            try
            {

                ReviewDto review = _reviewService.GetReviewById(Id);

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
