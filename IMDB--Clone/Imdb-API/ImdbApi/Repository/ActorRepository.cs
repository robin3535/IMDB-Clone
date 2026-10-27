using Dapper;
using ImdbApi.Models;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ImdbApi.Repository
{
    public class ActorRepository : BaseRepository<Actor>,IActorRepository
    {
        public ActorRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
        }
        public IEnumerable<Actor> Get()
        {
            string query = @"
SELECT [Id],
       [Name],
       [Bio],
       [DOB],
       [Sex] as [Gender]
FROM   [Foundation].[Actors] (NOLOCK)";
            return Get(query);
        }
        public Actor Get(int id)
        {
            string query = @"
SELECT [Id],
       [Name],
       [Bio],
       [DOB],
       [Sex] as [Gender]
FROM   [Foundation].[Actors] (NOLOCK) WHERE Id = @Id";
            return Get(query, new { Id = id });
        }
        public IEnumerable<Actor> GetActorByMovieId(int movieId)
        {
            var query = @"
SELECT
  [A].[Id],
  [A].[Name],
  [A].[Bio],
  [A].[DOB],
  [A].[Sex] AS [Gender]
FROM
  [Foundation].[Movies] M
  join [Foundation].[Actors_Movies] AM on [M].[Id] = [AM].[Movie_Id]
  join [Foundation].[Actors] A on [AM].[Actor_Id] = [A].[Id]
WHERE
  [M].[Id] = @Id;";
            return Gets(query, new { Id = movieId });
        }
        public int Create(ActorRequest actorRq)
        {
            return Create("usp_insert_actor", 
                new {
                    chvName = actorRq.Name,
                    txtBio = actorRq.Bio,
                    dtDOB = actorRq.DOB,
                    chrSex = actorRq.Gender
                });
        }
        public void Update(ActorRequest actorRq,int id)
        {
            Update("usp_update_actor",
              new
              {
                    intId = id,
                    chvName = actorRq.Name,
                    txtBio = actorRq.Bio,
                    dtDOB = actorRq.DOB,
                    chrSex = actorRq.Gender,
                    dtmUpdateAt = DateTime.Now
              });
        }
        public void Delete(int id)
        { 
            Delete("usp_delete_actor", new
            {
                intId = id
            });
        }

        public void UpdatePatch(JsonPatchDocument actorPatch, int id)
        {
            string query = @"
SELECT [Id],
       [Name],
       [Bio],
       [DOB],
       [Sex] 
FROM   [Foundation].[Actors] (NOLOCK) WHERE Id = @Id";
            var actor = Get(query, new { Id = id });
            actorPatch.ApplyTo(actor);

            Update("usp_update_actor",
             new
             {
                 intId = id,
                 chvName = actor.Name,
                 txtBio = actor.Bio,
                 dtDOB = actor.DOB,
                 chrSex = actor.Gender,
                 dtmUpdateAt = DateTime.Now
             });
        }

    }
}
