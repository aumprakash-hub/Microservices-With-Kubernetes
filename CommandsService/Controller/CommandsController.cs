using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controller;

[ApiController, Route("api/c/platforms/{platformId}/[controller]")]
public class CommandsController(ICommandRepo commandRepo, IMapper mapper) : ControllerBase
{
    private readonly ICommandRepo _commandRepo = commandRepo;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
        if (!_commandRepo.PlatformExists(platformId)) return NotFound();
        var commandsItem = _commandRepo.GetCommandsForPlatform(platformId);
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsItem));
    }

    [HttpGet, Route("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
        if (!_commandRepo.PlatformExists(platformId)) return NotFound();
        var commandItem = _commandRepo.GetCommand(platformId, commandId);
        if (commandItem == null) return NotFound();
        return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
    {
        if (!_commandRepo.PlatformExists(platformId)) return NotFound();
        var command = _mapper.Map<Command>(commandCreateDto);
        _commandRepo.CreateCommand(platformId,command);
        _commandRepo.SaveChanges();
        var commandReadDto = _mapper.Map<CommandReadDto>(command);
        return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
    } 
}