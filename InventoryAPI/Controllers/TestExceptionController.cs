using DH_GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestExceptionController : ControllerBase
    {
        [HttpGet("badrequest")]
        public IActionResult BadRequestTest()
        {
            throw new BadRequestException("Invalid inventory request.");
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTest()
        {
            throw new UnauthorizedException("You are not authorized to access inventory.");
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundTest()
        {
            throw new NotFoundException("Inventory record not found.");
        }

        [HttpGet("conflict")]
        public IActionResult ConflictTest()
        {
            throw new ConflictException("Inventory item already exists.");
        }

        [HttpGet("servererror")]
        public IActionResult ServerErrorTest()
        {
            throw new Exception("This is an unhandled inventory exception.");
        }
    }
}