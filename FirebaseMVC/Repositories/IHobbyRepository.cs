using NashIRL.Models;
using System.Collections.Generic;

namespace NashIRL.Repositories
{
    public interface IHobbyRepository
    {
        List<Hobby> GetAll();

        Hobby GetById(int id);
    }
}