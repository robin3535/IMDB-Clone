using System;

namespace ImdbApi.Models.RequestModel
{
    public class ActorRequest
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string UpdatedAt { get; set; }
    }
}
