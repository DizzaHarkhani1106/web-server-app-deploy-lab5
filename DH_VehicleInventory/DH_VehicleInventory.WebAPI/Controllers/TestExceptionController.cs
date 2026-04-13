using DH_GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DH_VehicleInventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestExceptionController : ControllerBase
    {
        [HttpGet("badrequest")]
        public IActionResult BadRequestTest()
        {
            throw new BadRequestException("Invalid vehicle request.");
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTest()
        {
            throw new UnauthorizedException("You are not authorized to access vehicles.");
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Vehicle record not found.");
        }

        [HttpGet("conflict")]
        public IActionResult ConflictTest()
        {
            throw new ConflictException("Vehicle already exists.");
        }

        [HttpGet("servererror")]
        public IActionResult ServerErrorTest()
        {
            throw new Exception("This is an unhandled vehicle exception.");
        }
    }
}