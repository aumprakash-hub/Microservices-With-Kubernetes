using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controller;
[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController(ICommandRepo commandRepo, IMapper mapper) : ControllerBase
{
    private readonly ICommandRepo _commandRepo = commandRepo;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var platformItems = _commandRepo.GetAllPlatforms();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }
    
}