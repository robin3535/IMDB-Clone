using System.Collections.Generic;
using ImdbApi.Exceptions;
using ImdbApi.Repository.Interfaces;
using ImdbApi.Services.Interfaces;
using System.Linq;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Models.RequestModel;
using AutoMapper;
using ImdbApi.Models.DB;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace ImdbApi.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorService(IActorRepository actorRepository,IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public IEnumerable<ActorResponse> Get()
        {
            return _mapper.Map<List<ActorResponse>>(_actorRepository.Get());
        }

        public ActorResponse Get(int id)
        {
            if (_actorRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Actor found with id= " + id);
            }
            return _mapper.Map<ActorResponse>(_actorRepository.Get(id));
        }
        public IEnumerable<Actor> GetActorByMovieId(int movieId)
        {
            if(_actorRepository.GetActorByMovieId(movieId) == null)
            {
                throw new RecordNotFoundException("No Actors found.");
            }
            return _actorRepository.GetActorByMovieId(movieId);
        }
        public int Create(ActorRequest actor)
        {
            if (Validate(actor))
            {
               return _actorRepository.Create(actor);
            }
            return 0;
        
        }
        public void Delete(int id)
        {
            if (_actorRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Actor found with id= " + id);
            }
            _actorRepository.Delete(id);
        }
        public void Update(ActorRequest actorRq,int id)
        {
            if (_actorRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Actor found with id= " + id);
            }
            if (Validate(actorRq))
            {
                _actorRepository.Update(actorRq,id);
            }
            
        }
        public void UpdatePatch(JsonPatchDocument actorPatch, int id)
        {
            if (_actorRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Actor found with id= " + id);
            }
            _actorRepository.UpdatePatch(actorPatch, id);
        }
        public bool Validate(ActorRequest actorRq)
        {
          
            if (string.IsNullOrEmpty(actorRq.Name))
            {
                throw new FieldValueNullException("Name cannot be empty.");
            }
            if (int.TryParse(actorRq.Name, out int rName))
            {
                throw new InvalidFieldValueException("Name cannot be integer.");
            }
            if (string.IsNullOrEmpty(actorRq.Bio))
            {
                throw new FieldValueNullException("Bio cannot be empty.");
            }
            if (string.IsNullOrEmpty(actorRq.Gender))
            {
                throw new FieldValueNullException("Gender cannot be empty.");
            }
            if(!DateTime.TryParse(actorRq.DOB, out DateTime tempDob))
            {
                throw new InvalidFieldValueException("DOB is empty/Invalid.");
            }
            return true;
        }

       
    }
   
}
