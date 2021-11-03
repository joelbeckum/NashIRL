using NashIRL.Models;
using System.Collections.Generic;

namespace NashIRL.Repositories
{
    public interface IHobbyRepository
    {
        List<Hobby> GetAll();

        Hobby GetById(int id);

        void Add(Hobby hobby);

        void Approve(int id, int adminId);

        void Update(Hobby hobby);

        void Delete(int id);
    }
}