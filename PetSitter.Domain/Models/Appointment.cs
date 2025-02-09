namespace PetSitter.Domain.Models;


public enum ServiceTypes
{
    DogWalking,
    HomeVisit,
}


public abstract class AppointmentBase
{
    public required ServiceTypes ServiceType { get; set; }
    public required DateTime AppointmentDateTime { get; set; }
    public required Guid PetId { get; set; }
    public required Guid SitterId { get; set; }
}


public class Appointment : AppointmentBase
{
    public Guid AppointmentId { get; set; } = Guid.NewGuid();
}
