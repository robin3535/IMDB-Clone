using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ImdbApi.Repository
{
    public class ProducerRepository :BaseRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
            
        }
        public IEnumerable<Producer> Get()
        {
            string query = @"
SELECT [Id],
       [Name],
       [Bio],
       [DOB],
       [Sex] as [Gender]
FROM   [Foundation].[Producers] (NOLOCK)";
            return Get(query);
        }
        public Producer Get(int id)
        {
            string query = @"
SELECT [Id],
       [Name],
       [Bio],
       [DOB],
       [Sex] as [Gender]
FROM   [Foundation].[Producers] (NOLOCK) WHERE Id = @Id";
            return Get(query, new { Id = id });
        }
        public int Create(ProducerRequest producerRq)
        {
            return Create("usp_insert_producer",
              new
              {
                  chvName = producerRq.Name,
                  txtBio = producerRq.Bio,
                  dtDOB = producerRq.DOB,
                  chrSex = producerRq.Gender
              });
        }
        public void Update(ProducerRequest producerRq,int id)
        {
            
            Update("usp_update_producer",
              new
              {
                  intId = id,
                  chvName = producerRq.Name,
                  txtBio = producerRq.Bio,
                  dtDOB = producerRq.DOB,
                  chrSex = producerRq.Gender,
                  dtmUpdateAt = DateTime.Now
              });

        }
        public void Delete(int id)
        {
            Delete("usp_delete_producer", new
            {
                intId = id
            });
        }


    }
}
