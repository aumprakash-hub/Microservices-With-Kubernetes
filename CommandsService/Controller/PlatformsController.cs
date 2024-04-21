using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controller;
[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {
        
    }

    [HttpGet]
    public ActionResult TestInboud()
    {
        Console.WriteLine("-->Inbound call");
        return Ok();
    }
    
}