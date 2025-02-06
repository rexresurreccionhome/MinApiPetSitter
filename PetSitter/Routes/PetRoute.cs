namespace PetSitter.Routes;

using Microsoft.AspNetCore.Http.HttpResults;

using PetSitter.Domain.Models;
using PetSitter.Domain.Interface;
using PetSitter.DataStore;
using PetSitter.Models;


public static class PetRoute 
{
    public const string baseUrl = "/pets";

    public static RouteGroupBuilder MapPetApi(this RouteGroupBuilder group)
    {
        IDbConnectionFactory dbConnectionFactory = new DbConnectionFactory("Server=localhost;Database=PetSitterDataStore; User Id=sa;Password=Secured*;Pooling=false;TrustServerCertificate=true;Trusted_Connection=false");
        IPetRepository petRepository = new PetRepository(dbConnectionFactory);
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

