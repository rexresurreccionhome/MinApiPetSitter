namespace PetSitter.Routes;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using PetSitter.Domain.Interface;
using PetSitter.Domain.Models;
using PetSitter.Models;


public static class AppointmentRoute
{
    public const string baseUrl = "/appointments";

    public static RouteGroupBuilder MapAppointmentApi(this RouteGroupBuilder group)
    {
        AppointmentHandler appointmentHandler = new();
        group.MapGet("", appointmentHandler.GetAppointments);
        group.MapPost("", appointmentHandler.CreateAppointment);
        group.MapGet("/{appointmentId}", appointmentHandler.GetAppointment);
        group.MapPut("/{appointmentId}", appointmentHandler.UpdateAppointment);
        group.MapDelete("/{appointmentId}", appointmentHandler.DeleteAppointment);

        return group;
    }

    public class AppointmentHandler
    {

        public Ok<GetApppointmentsResponse> GetAppointments(IAppointmentRepository appointmentRepository, [FromQuery(Name = "serviceType")] ServiceTypes? serviceType = null, [FromQuery(Name = "startDateTime")] DateTime? startDateTime = null, [FromQuery(Name = "endDateTime")] DateTime? endDateTime = null)
        {
            return TypedResults.Ok(
                new GetApppointmentsResponse
                {
                    Appointments = appointmentRepository.GetAppointments(
                        serviceType: serviceType,
                        startDateTime: startDateTime,
                        endDateTime: endDateTime
                    ),
                }
            );
        }

        public Results<Ok<Appointment>, NotFound> GetAppointment(Guid appointmentId, IAppointmentRepository appointmentRepository)
        {
            Appointment? appointment = appointmentRepository.GetAppointment(appointmentId);
            return appointment is not null ? TypedResults.Ok(appointment) : TypedResults.NotFound();
        }

        public Results<Created<Appointment>, Created> CreateAppointment(AppointmentInput appointmentInput, IAppointmentRepository appointmentRepository)
        {
            Appointment appointment = appointmentRepository.CreateAppointment(appointmentInput);
            return TypedResults.Created($"{AppointmentRoute.baseUrl}/{appointment.AppointmentId}", appointment);
        }

        public Results<Ok<Appointment>, NotFound> UpdateAppointment(Guid appointmentId, AppointmentInput appointmentInput, IAppointmentRepository appointmentRepository)
        {
            Appointment? appointment = appointmentRepository.UpdateAppointment(appointmentId, appointmentInput);
            return appointment is not null ? TypedResults.Ok(appointment) : TypedResults.NotFound();
        }

        public Results<EmptyHttpResult, NoContent> DeleteAppointment(Guid appointmentId, IAppointmentRepository appointmentRepository)
        {
            appointmentRepository.DeleteAppointment(appointmentId);
            return TypedResults.NoContent();
        }
    }
}

