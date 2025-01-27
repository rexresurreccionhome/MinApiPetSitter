namespace PetSitter.Routes;

using Microsoft.AspNetCore.Http.HttpResults;

using PetSitter.Models;
using PetSitter.DB;
using PetSitter.Repository;


public static class PetRoute 
{
    public const string baseUrl = "/pets";

    public static RouteGroupBuilder MapPetApi(this RouteGroupBuilder group)
    {
        PetRepository petRepository = new(PetDB.Pets);
        PetHandler petHandler = new(petRepository);
        group.MapGet("", petHandler.GetPets);
        group.MapPost("", petHandler.CreatePet);
        group.MapGet("/{petId}", petHandler.GetPet);
        group.MapPut("/{petId}", petHandler.UpdatePet);
        group.MapDelete("/{petId}", petHandler.DeletePet);

        return group;
    }

    public class PetHandler 
    {
        private readonly IPetRepository _petRepository;

        public PetHandler(IPetRepository petRepository) {
            _petRepository = petRepository;
        }

        public Ok<GetPetsResponse> GetPets()
        {
            return TypedResults.Ok(new GetPetsResponse{Pets=_petRepository.GetPets()});
        }

        public Results<Ok<Pet>, NotFound> GetPet(Guid petId)
        {
            Pet? pet = _petRepository.GetPet(petId);
            return pet is not null ? TypedResults.Ok(pet) : TypedResults.NotFound();
        }

        public Results<Created<Pet>, Created> CreatePet(PetInput petInput)
        {
            Pet pet = _petRepository.CreatePet(petInput);
            return TypedResults.Created($"{PetRoute.baseUrl}/{pet.PetId}", pet);
        }

        public Results<Ok<Pet>, NotFound> UpdatePet(Guid petId, PetInput petInput)
        {
            Pet? pet = _petRepository.UpdatePet(petId, petInput);
            return pet is not null ? TypedResults.Ok(pet) : TypedResults.NotFound();
        }

        public Results<EmptyHttpResult, NoContent> DeletePet(Guid petId)
        {
            _petRepository.DeletePet(petId);
            return TypedResults.NoContent();
        }
    }
}

