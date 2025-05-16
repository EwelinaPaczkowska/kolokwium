using kolokwium.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController: ControllerBase
{
    private readonly IAppointmentsService _appointmentsService;
    
    public AppointmentsController(IAppointmentsService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(int id, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentsService.GetAppointmentById(id, cancellationToken);
        return Ok(appointment);
    }
}