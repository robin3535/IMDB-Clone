using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository;
using ImdbApi.Repository.Interfaces;
using ImdbApi.Services.Interfaces;

namespace ImdbApi.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;
        public ProducerService(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }
        public IEnumerable<ProducerResponse> Get()
        {
            return _mapper.Map<List<ProducerResponse>>(_producerRepository.Get());
        }
        public ProducerResponse Get(int id)
        {
            if (_producerRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Producer found with id= " + id);
            }
            return _mapper.Map<ProducerResponse>(_producerRepository.Get(id));
        }
       
        public int Create(ProducerRequest producer)
        {
            if (Validate(producer))
            {
                return _producerRepository.Create(producer);
            }
            return 0;
        }
        public void Delete(int id)
        {
            if (_producerRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Producer found with id= " + id);
            }
            _producerRepository.Delete(id);
        }
        
        public void Update(ProducerRequest producerRq, int id)
        {
            if (_producerRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Producer found with id= " + id);
            }

            if (Validate(producerRq))
            {
                _producerRepository.Update(producerRq,id);
            }
        }
        public bool Validate(ProducerRequest producerRq)
        {
            if (string.IsNullOrEmpty(producerRq.Name))
            {
                throw new FieldValueNullException("Name cannot be empty.");
            }
            if (int.TryParse(producerRq.Name, out int rName))
            {
                throw new InvalidFieldValueException("Name cannot be integer.");
            }
            if (string.IsNullOrEmpty(producerRq.Bio))
            {
                throw new FieldValueNullException("Bio cannot be empty.");
            }
            if (string.IsNullOrEmpty(producerRq.Gender))
            {
                throw new FieldValueNullException("Gender cannot be empty.");
            }
            if (!DateTime.TryParse(producerRq.DOB, out DateTime tempDob))
            {
                throw new InvalidFieldValueException("DOB is empty/Invalid.");
            }
            return true;
        }
    }
}
