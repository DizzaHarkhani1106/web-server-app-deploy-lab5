using DH_GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestExceptionController : ControllerBase
    {
        [HttpGet("badrequest")]
        public IActionResult BadRequestTest()
        {
            throw new BadRequestException("Invalid request data.");
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTest()
        {
            throw new UnauthorizedException("You are not authorized.");
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Requested record was not found.");
        }

        [HttpGet("conflict")]
        public IActionResult ConflictTest()
        {
            throw new ConflictException("Duplicate record already exists.");
        }

        [HttpGet("servererror")]
        public IActionResult ServerErrorTest()
        {
            throw new Exception("This is an unhandled exception.");
        }
    }
}