using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data;

public class CommandRepo(AppDbContext context) : ICommandRepo
{
    private readonly AppDbContext _context = context;

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    #region Platform Section

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null) throw new ArgumentNullException(nameof(platform), "Platform object has null value");
        _context.Platforms.Add(platform);
    }

    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(p => p.Id == platformId);
    }

    #endregion

    #region Command Section

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
    }

    public void CreateCommand(int platformId, Command command)
    {
        if (command == null) throw new ArgumentNullException(nameof(command), "Command object has null value");
        command.PlatformId = platformId;
        _context.Commands.Add(command);
    }

    #endregion
   
}