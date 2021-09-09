using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exterminator.WebApi.Controllers
{
  [Route("api/logs")]
  public class LogController : Controller
  {
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
      _logService = logService;
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetAllLogs() => Ok(_logService.GetAllLogs());
  }
}