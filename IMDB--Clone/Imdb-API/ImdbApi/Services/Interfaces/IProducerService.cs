
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using System.Collections.Generic;

namespace ImdbApi.Services.Interfaces
{
    public interface IProducerService
    {
        IEnumerable<ProducerResponse> Get();
        ProducerResponse Get(int id);
        int Create(ProducerRequest producer);
        void Update(ProducerRequest producer, int id);
        void Delete(int id);
    }
}
