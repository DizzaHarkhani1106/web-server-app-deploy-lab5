using DH_GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestExceptionController : ControllerBase
    {
        [HttpGet("badrequest")]
        public IActionResult BadRequestTest()
        {
            throw new BadRequestException("Invalid maintenance request.");
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTest()
        {
            throw new UnauthorizedException("You are not authorized to access maintenance.");
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Maintenance record not found.");
        }

        [HttpGet("conflict")]
        public IActionResult ConflictTest()
        {
            throw new ConflictException("Maintenance record already exists.");
        }

        [HttpGet("servererror")]
        public IActionResult ServerErrorTest()
        {
            throw new Exception("This is an unhandled maintenance exception.");
        }
    }
}