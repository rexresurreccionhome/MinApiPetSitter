namespace PetSitter.Routes;

using Microsoft.AspNetCore.Http.HttpResults;

using PetSitter.Models;
using PetSitter.DB;
using PetSitter.Repository;


public static class SitterRoute 
{
    public const string baseUrl = "/sitters";

    public static RouteGroupBuilder MapSitterApi(this RouteGroupBuilder group)
    {
        SitterRepository sitterRepository = new(SitterDB.Sitters);
        SitterHandler sitterHandler = new(sitterRepository);
        group.MapGet("", sitterHandler.GetSitters);
        group.MapPost("", sitterHandler.Createsitter);
        group.MapGet("/{sitterId}", sitterHandler.GetSitter);
        group.MapPut("/{sitterId}", sitterHandler.UpdateSitter);
        group.MapDelete("/{sitterId}", sitterHandler.DeleteSitter);

        return group;
    }

    public class SitterHandler 
    {
        private readonly SitterRepository _sitterRepository;

        public SitterHandler(SitterRepository sitterRepository) {
            _sitterRepository = sitterRepository;
        }

        public Ok<GetSittersResponse> GetSitters()
        {
            return TypedResults.Ok(new GetSittersResponse{ Sitters = _sitterRepository.GetSitters() });
        }

        public Results<Ok<Sitter>, NotFound> GetSitter(Guid sitterId)
        {
            Sitter? sitter = _sitterRepository.GetSitter(sitterId);
            return sitter is not null ? TypedResults.Ok(sitter) : TypedResults.NotFound();
        }

        public Results<Created<Sitter>, Created> Createsitter(SitterInput sitterInput)
        {
            Sitter sitter = _sitterRepository.CreateSitter(sitterInput);
            return TypedResults.Created($"{SitterRoute.baseUrl}/{sitter.SitterId}", sitter);
        }

        public Results<Ok<Sitter>, NotFound> UpdateSitter(Guid sitterId, SitterInput sitterInput)
        {
            Sitter? sitter = _sitterRepository.UpdateSitter(sitterId, sitterInput);
            return sitter is not null ? TypedResults.Ok(sitter) : TypedResults.NotFound();
        }

        public Results<EmptyHttpResult, NoContent> DeleteSitter(Guid sitterId)
        {
            _sitterRepository.DeleteSitter(sitterId);
            return TypedResults.NoContent();
        }
    }
}

