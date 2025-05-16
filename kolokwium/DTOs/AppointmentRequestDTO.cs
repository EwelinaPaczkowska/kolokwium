namespace kolokwium.DTOs;

public class AppointmentRequestDTO
{
    public PatientDTO patient;
    public DoctorDTO doctor;
    public DateTime date;
    public int doctorId { get; set; }
    public int patientId { get; set; }
    public int appointmentId { get; set; } 
    public List<ServiceDTO> Services { get; set; }
}