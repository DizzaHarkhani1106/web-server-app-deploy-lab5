using DH_GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DH_ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestExceptionController : ControllerBase
    {
        [HttpGet("badrequest")]
        public IActionResult BadRequestTest()
        {
            throw new BadRequestException("Invalid gateway request.");
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTest()
        {
            throw new UnauthorizedException("You are not authorized to access gateway.");
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Gateway route not found.");
        }

        [HttpGet("conflict")]
        public IActionResult ConflictTest()
        {
            throw new ConflictException("Gateway request conflict occurred.");
        }

        [HttpGet("servererror")]
        public IActionResult ServerErrorTest()
        {
            throw new Exception("This is an unhandled gateway exception.");
        }
    }
}