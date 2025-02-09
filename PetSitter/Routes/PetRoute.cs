namespace PetSitter.Routes;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using PetSitter.Domain.Interface;
using PetSitter.Domain.Models;
using PetSitter.Models;

public static class PetRoute
{
    public const string baseUrl = "/pets";

    public static RouteGroupBuilder MapPetApi(this RouteGroupBuilder group)
    {
        PetHandler petHandler = new();
        group.MapGet("", petHandler.GetPets);
        group.MapPost("", petHandler.CreatePet);
        group.MapGet("/{petId}", petHandler.GetPet);
        group.MapPut("/{petId}", petHandler.UpdatePet);
        group.MapDelete("/{petId}", petHandler.DeletePet);
        group.MapGet("/{petId}/appointments", petHandler.GetAppointments);

        return group;
    }

    public class PetHandler
    {
        public Ok<GetPetsResponse> GetPets(IPetRepository petRepository)
        {
            return TypedResults.Ok(new GetPetsResponse { Pets = petRepository.GetPets() });
        }

        public Results<Ok<Pet>, NotFound> GetPet(Guid petId, IPetRepository petRepository)
        {
            Pet? pet = petRepository.GetPet(petId);
            return pet is not null ? TypedResults.Ok(pet) : TypedResults.NotFound();
        }

        public Results<Created<Pet>, Created> CreatePet(PetInput petInput, IPetRepository petRepository)
        {
            Pet pet = petRepository.CreatePet(petInput);
            return TypedResults.Created($"{PetRoute.baseUrl}/{pet.PetId}", pet);
        }

        public Results<Ok<Pet>, NotFound> UpdatePet(Guid petId, PetInput petInput, IPetRepository petRepository)
        {
            Pet? pet = petRepository.UpdatePet(petId, petInput);
            return pet is not null ? TypedResults.Ok(pet) : TypedResults.NotFound();
        }

        public Results<EmptyHttpResult, NoContent> DeletePet(Guid petId, IPetRepository petRepository)
        {
            petRepository.DeletePet(petId);
            return TypedResults.NoContent();
        }

        public Ok<GetApppointmentsResponse> GetAppointments(Guid petId, IAppointmentRepository appointmentRepository, [FromQuery(Name = "serviceType")] ServiceTypes? serviceType = null)
        {
            return TypedResults.Ok(
                new GetApppointmentsResponse
                {
                    Appointments = appointmentRepository.GetAppointmentsByPet(petId, serviceType: serviceType),
                }
            );
        }
    }
}

