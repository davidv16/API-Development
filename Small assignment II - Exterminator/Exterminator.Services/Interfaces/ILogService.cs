using System.Collections.Generic;
using Exterminator.Models;
using Exterminator.Models.Dtos;

namespace Exterminator.Services.Interfaces
{
  public interface ILogService
  {
    void LogToDatabase(ExceptionModel exception);
    IEnumerable<LogDto> GetAllLogs();
  }
}