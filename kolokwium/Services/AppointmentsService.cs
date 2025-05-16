using kolokwium.DTOs;
using kolokwium.Exceptions;
using kolokwium.Models;
using kolokwium.Repositories;

namespace kolokwium.Services;

public class AppointmentsService : IAppointmentsService
{
    private readonly IAppoinmentsRepository _deliveriesRepository;
    public AppointmentsService(IAppoinmentsRepository deliveriesRepository)
    {
        _deliveriesRepository = deliveriesRepository;
    }

    public async Task<AppointmentRequestDTO> GetAppointmentById(int id, CancellationToken cancellationToken)
    {
        if(id<0)
            throw new BadRequestException("id musi byc wieksze niz 0");
        
        return await _deliveriesRepository.GetAppointmentById(id, cancellationToken);
    }
}