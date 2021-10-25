using NashIRL.Models;
using System.Collections.Generic;

namespace NashIRL.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetAll();

        List<Event> GetByHobbyId(int hobbyId);

        Event GetById(int id);

        void Add(Event newEvent);

        void Update(Event newEvent);

        void Delete(int id);
    }
}