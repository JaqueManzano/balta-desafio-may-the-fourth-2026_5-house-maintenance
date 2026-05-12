using HomeMaintenance.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeTasksController : ControllerBase
{
    private readonly IHomeTaskService _service;

    public HomeTasksController(IHomeTaskService service)
    {
        _service = service;
    }

    [HttpPost("organize")]
    public async Task<IActionResult> Organize(
        [FromBody] OrganizeTasksRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.SendAsync(
            request.Text,
            cancellationToken);

        return Ok(result);
    }
}
