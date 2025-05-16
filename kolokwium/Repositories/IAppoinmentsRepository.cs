using kolokwium.DTOs;
//using kolokwium.Models;

namespace kolokwium.Repositories;

public interface IAppoinmentsRepository
{
    public Task<AppointmentRequestDTO> GetAppointmentById(int id, CancellationToken cancellationToken);
    
}