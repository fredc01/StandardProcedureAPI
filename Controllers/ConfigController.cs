using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;
    public ConfigController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult GetApiUrls()
    {
        return Ok(new
        {
            Local = _config["ApiUrls:Local"],
            Test = _config["ApiUrls:Test"],
            Live = _config["ApiUrls:Live"]
        });
    }
}
