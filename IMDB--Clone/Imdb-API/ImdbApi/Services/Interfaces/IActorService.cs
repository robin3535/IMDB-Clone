using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace ImdbApi.Services.Interfaces
{
    public interface IActorService
    {
        IEnumerable<ActorResponse> Get();
        ActorResponse Get(int id);
        int Create(ActorRequest actor);
        void Update(ActorRequest actor,int id);
        void UpdatePatch(JsonPatchDocument actorPatch, int id);
        IEnumerable<Actor> GetActorByMovieId(int movieId);
        void Delete(int id);
    }
}
