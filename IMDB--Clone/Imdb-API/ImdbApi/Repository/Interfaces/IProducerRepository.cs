using System.Collections.Generic;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;

namespace ImdbApi.Repository.Interfaces
{
    public interface IProducerRepository
    {
        int Create(ProducerRequest producer);
        void Delete(int id);
        IEnumerable<Producer> Get();
        Producer Get(int id);
        void Update(ProducerRequest producer,int id);
    }
}