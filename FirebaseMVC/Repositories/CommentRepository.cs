using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NashIRL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Utils;

namespace NashIRL.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

        public List<Comment> GetByEvent(int eventId)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT c.Id, c.Body, c.UserProfileId, c.EventId, c.CreatedOn,
	                       up.FirstName, up.LastName, up.Email, up.ImageUrl
                    FROM Comment c
                    LEFT JOIN UserProfile up ON c.UserProfileId = up.Id
                    WHERE c.EventId = @eventId;";

            DbUtils.AddParameter(cmd, "@eventId", eventId);

            var comments = new List<Comment>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Comment comment = null;
                comment = AssignNewComment(reader, comment);

                comments.Add(comment);
            }

            return comments;
        }

        private Comment AssignNewComment(SqlDataReader reader, Comment comment)
        {
            comment = new Comment()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Body = DbUtils.GetString(reader, "Body"),
                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                EventId = DbUtils.GetInt(reader, "EventId"),
                CreatedOn = DbUtils.GetDateTime(reader, "CreatedOn")
            };
            comment.UserProfile = new UserProfile()
            {
                Id = DbUtils.GetInt(reader, "UserProfileId"),
                FirstName = DbUtils.GetString(reader, "FirstName"),
                LastName = DbUtils.GetString(reader, "LastName"),
                Email = DbUtils.GetString(reader, "Email"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl")
            };

            return comment;
        }
    }
}
