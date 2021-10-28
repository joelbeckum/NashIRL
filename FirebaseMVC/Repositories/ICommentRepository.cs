using NashIRL.Models;
using System.Collections.Generic;

namespace NashIRL.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetByEvent(int eventId);

        Comment GetById(int id);

        void Add(Comment comment);

        void Update(Comment comment);
    }
}