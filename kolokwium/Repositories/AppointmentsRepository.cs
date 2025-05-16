using kolokwium.DTOs;
using Microsoft.Data.SqlClient;

namespace kolokwium.Repositories;

public class AppoinmentsRepository: IAppoinmentsRepository
{
    private readonly string _connectionString;

    public AppoinmentsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<AppointmentRequestDTO> GetAppointmentById(int id, CancellationToken cancellationToken)
    {
        var appointment = new AppointmentRequestDTO();
        appointment.Services = new List<ServiceDTO>();
        
        await using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken);

            var query = @"SELECT 
                        [dbo].[Appointment].[date],
                        [dbo].[Patient].[first_name],
                        [dbo].[Patient].[last_name],
                        [dbo].[Patient].[date_of_birth],
                        [dbo].[Doctor].[doctor_id],
                        [dbo].[Doctor].[PWZ],
                        [dbo].[Service].[name],
                        [dbo].[Service].[base_fee]
                        FROM [dbo].[Appointment]
                        Inner Join [dbo].[Appointment_Service] On [dbo].[Appointment].[appoitment_id] = [dbo].[Appointment_Service].[appoitment_id]
                        Inner Join [dbo].[Service] On [dbo].[Service].[service_id] = [dbo].[Appointment_Service].[service_id]
                        Inner Join [dbo].[Patient] On [dbo].[Patient].[patient_id] = [dbo].[Appointment].[patient_id]
                        Inner Join [dbo].[Doctor] On Doctor.doctor_id = Appointment.doctor_id
                        WHERE [dbo].[Appointment].[appoitment_id] = @id";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        appointment.patient = new PatientDTO()
                        {
                            first_name = reader.GetString(reader.GetOrdinal("first_name")),
                            last_name = reader.GetString(reader.GetOrdinal("last_name")),
                            date_of_birth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                        };
                        appointment.doctor = new DoctorDTO()
                        {
                            doctor_id = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                            PWZ = reader.GetString(reader.GetOrdinal("licence_number")),
                        };
                        var service = new ServiceDTO()
                        {
                            name = reader.GetString(reader.GetOrdinal("name")),
                            base_fee = reader.GetDecimal(reader.GetOrdinal("base_fee")),
                        };
                        appointment.Services.Add(service);
                        appointment.date = reader.GetDateTime(reader.GetOrdinal("date"));
                    }
                }
            }
        }

        return appointment;
    }
    public async Task<bool> DoesAppointmentExistAsync(int id, CancellationToken cancellationToken)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {

            await connection.OpenAsync(cancellationToken);

            var query = @"SELECT COUNT(1) FROM [dbo].[Appointment] WHERE appointment_id = @appointment_id";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@delivery_id", id);

                var result = await command.ExecuteScalarAsync(cancellationToken);

                return Convert.ToInt32(result) > 0;
            }
        }
    }


    public async Task<bool> DoesPatientExistAsync(int id, CancellationToken cancellationToken)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {

            await connection.OpenAsync(cancellationToken);

            var query = @"SELECT COUNT(1) FROM [dbo].[Patient] WHERE patient_id = @patient_id";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@customer_id", id);

                var result = await command.ExecuteScalarAsync(cancellationToken);

                return Convert.ToInt32(result) > 0;
            }
        }
    }

    public async Task<bool> DoesDoctorExistAsync(int id, CancellationToken cancellationToken)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {

            await connection.OpenAsync(cancellationToken);

            var query = @"SELECT COUNT(1) FROM [dbo].[Doctor] WHERE doctor_id = @doctor_id";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@driver_id", id);

                var result = await command.ExecuteScalarAsync(cancellationToken);

                return Convert.ToInt32(result) > 0;
            }
        }
    }

    public async Task<bool> DoesServiceNameExistAsync(String name, CancellationToken cancellationToken)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {

            await connection.OpenAsync(cancellationToken);

            var query = @"SELECT COUNT(1) FROM [dbo].[Service] WHERE name = @name";

            await using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                var result = await command.ExecuteScalarAsync(cancellationToken);

                return Convert.ToInt32(result) > 0;
            }
        }
    }
}