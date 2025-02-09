namespace PetSitter.Routes;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using PetSitter.Domain.Interface;
using PetSitter.Domain.Models;
using PetSitter.Models;


public static class SitterRoute
{
    public const string baseUrl = "/sitters";

    public static RouteGroupBuilder MapSitterApi(this RouteGroupBuilder group)
    {
        SitterHandler sitterHandler = new();
        group.MapGet("", sitterHandler.GetSitters);
        group.MapPost("", sitterHandler.Createsitter);
        group.MapGet("/{sitterId}", sitterHandler.GetSitter);
        group.MapPut("/{sitterId}", sitterHandler.UpdateSitter);
        group.MapDelete("/{sitterId}", sitterHandler.DeleteSitter);
        group.MapGet("/{sitterId}/appointments", sitterHandler.GetAppointments);

        return group;
    }

    public class SitterHandler
    {
        public Ok<GetSittersResponse> GetSitters(ISitterRepository sitterRepository)
        {
            return TypedResults.Ok(new GetSittersResponse { Sitters = sitterRepository.GetSitters() });
        }

        public Results<Ok<Sitter>, NotFound> GetSitter(Guid sitterId, ISitterRepository sitterRepository)
        {
            Sitter? sitter = sitterRepository.GetSitter(sitterId);
            return sitter is not null ? TypedResults.Ok(sitter) : TypedResults.NotFound();
        }

        public Results<Created<Sitter>, Created> Createsitter(SitterInput sitterInput, ISitterRepository sitterRepository)
        {
            Sitter sitter = sitterRepository.CreateSitter(sitterInput);
            return TypedResults.Created($"{SitterRoute.baseUrl}/{sitter.SitterId}", sitter);
        }

        public Results<Ok<Sitter>, NotFound> UpdateSitter(Guid sitterId, SitterInput sitterInput, ISitterRepository sitterRepository)
        {
            Sitter? sitter = sitterRepository.UpdateSitter(sitterId, sitterInput);
            return sitter is not null ? TypedResults.Ok(sitter) : TypedResults.NotFound();
        }

        public Results<EmptyHttpResult, NoContent> DeleteSitter(Guid sitterId, ISitterRepository sitterRepository)
        {
            sitterRepository.DeleteSitter(sitterId);
            return TypedResults.NoContent();
        }

        public Ok<GetApppointmentsResponse> GetAppointments(Guid sitterId, IAppointmentRepository appointmentRepository, [FromQuery(Name = "serviceType")] ServiceTypes? serviceType = null)
        {
            return TypedResults.Ok(
                new GetApppointmentsResponse
                {
                    Appointments = appointmentRepository.GetAppointmentsBySitter(sitterId, serviceType: serviceType),
                }
            );
        }
    }
}

