using kolokwium.DTOs;
//using kolokwium.Models;

namespace kolokwium.Repositories;

public interface IAppoinmentsRepository
{
    public Task<AppointmentRequestDTO> GetAppointmentById(int id, CancellationToken cancellationToken);
    //public Task<bool> DoesClientExistAsync(int id, CancellationToken cancellationToken);
    //public Task<bool> DoesDeliveryExistAsync(int id, CancellationToken cancellationToken);
    //public Task<bool> DoesDriverExistAsync(int id, CancellationToken cancellationToken);
    //public Task<bool> DoesProductNameExistAsync(String name, CancellationToken cancellationToken);
    
    //public Task<int> GetDriverIdForLicenceNumber(string licenceNumber, CancellationToken cancellationToken);
    
    //public Task<> GetProductByProductName(string productName, CancellationToken cancellationToken);
    
    //public Task<int> SaveDelivery(DeliverySaveDTO deliverySave, CancellationToken cancellationToken);
}