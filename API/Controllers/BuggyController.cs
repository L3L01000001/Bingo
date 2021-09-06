using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _db;
        public BuggyController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet("testauth")]
        //[Authorize]
        public ActionResult<string> GetSecretText(){
            return "secret info";
        }


        [HttpGet("notfound")]
        public ActionResult GetNotFound(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                //error code: 404 Not found
                return NotFound(new ApiResponse(404));
            }
            
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError(int id)
        {
            var product = _db.Products.Find(id);

            //exception error code: 500
          var productString = product.ToString();

            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
           // error code: 400
           return BadRequest(new ApiResponse(400));

        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            
            //validation problem on id
            return ValidationProblem();
        }
    }
}