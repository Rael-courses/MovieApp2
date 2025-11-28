using Microsoft.AspNetCore.Mvc;

namespace MovieAppApi.Src.Controllers;

public class HealthController : BaseController<HealthController>
{
  public HealthController(ILogger<HealthController> logger) : base(logger)
  {
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Ok("Ok");
  }
}