namespace PetSitter.Models;

using PetSitter.Domain.Models;


public class AppointmentInput : AppointmentBase;


public class GetApppointmentsResponse
{
    public List<Appointment> Appointments { get; set; } = [];
}