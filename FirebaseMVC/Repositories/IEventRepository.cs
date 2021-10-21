using NashIRL.Models;
using System.Collections.Generic;

namespace NashIRL.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetAll();
    }
}