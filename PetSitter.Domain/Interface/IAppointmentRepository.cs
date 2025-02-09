namespace PetSitter.Domain.Interface;

using PetSitter.Domain.Models;


public interface IAppointmentRepository
{
    public Appointment? GetAppointment(Guid appointmentId);
    public List<Appointment> GetAppointments(ServiceTypes? serviceType = null, DateTime? startDateTime = null, DateTime? endDateTime = null);
    public List<Appointment> GetAppointmentsByPet(Guid petId, ServiceTypes? serviceType = null);
    public List<Appointment> GetAppointmentsBySitter(Guid sitterId, ServiceTypes? serviceType = null);
    public Appointment CreateAppointment(AppointmentBase appointmentInput);
    public Appointment UpdateAppointment(Guid appointmentId, AppointmentBase appointmentInput);
    public void DeleteAppointment(Guid appointmentId);
}