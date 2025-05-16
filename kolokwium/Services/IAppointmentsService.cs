using kolokwium.DTOs;

namespace kolokwium.Services;

public interface IAppointmentsService
{
    public Task<AppointmentRequestDTO> GetAppointmentById(int id, CancellationToken cancellationToken);
}