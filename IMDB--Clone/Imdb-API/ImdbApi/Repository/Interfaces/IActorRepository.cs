using System.Collections.Generic;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using Microsoft.AspNetCore.JsonPatch;

namespace ImdbApi.Repository.Interfaces
{
    public interface IActorRepository
    {
        int Create(ActorRequest actor);
        void Delete(int id);
       IEnumerable<Actor> Get();
        Actor Get(int id);
        IEnumerable<Actor> GetActorByMovieId(int movieId);
        void Update(ActorRequest actor,int id);
        void UpdatePatch(JsonPatchDocument actorPatch,int id);  
    }
}