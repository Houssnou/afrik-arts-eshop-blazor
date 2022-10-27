using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugsController : BaseApiController
    {

        public StoreDbContext _context;
        public BugsController(StoreDbContext context)
        {
            _context = context;

        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> TestAuth()
        {
            return "Authorized.";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(50);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(50);

            var productToReturn = product.ToString();

            return Ok(productToReturn);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}