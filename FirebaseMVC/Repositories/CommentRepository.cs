using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NashIRL.Models;
using System.Collections.Generic;
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
                comment = AssembleComment(reader, comment);

                comments.Add(comment);
            }

            return comments;
        }

        public Comment GetById(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT c.Id, c.Body, c.UserProfileId, c.EventId, c.CreatedOn,
	                       up.FirstName, up.LastName, up.Email, up.ImageUrl
                    FROM Comment c
                    LEFT JOIN UserProfile up ON c.UserProfileId = up.Id
                    WHERE c.Id = @id";

            DbUtils.AddParameter(cmd, "@id", id);

            Comment comment = null;

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                comment = AssembleComment(reader, comment);
            }

            return comment;
        }

        public void Add(Comment comment)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    INSERT INTO Comment (Body, UserProfileId, EventId, CreatedOn)
                    OUTPUT INSERTED.Id
                    VALUES (@body, @userProfileId, @eventId, GETDATE())";

            DbUtils.AddParameter(cmd, "@body", comment.Body);
            DbUtils.AddParameter(cmd, "@userProfileId", comment.UserProfileId);
            DbUtils.AddParameter(cmd, "@eventId", comment.EventId);

            comment.Id = (int)cmd.ExecuteScalar();
        }

        public void Update(Comment comment)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    UPDATE Comment
                    SET Body = @body
                    WHERE Comment.Id = @id;";

            DbUtils.AddParameter(cmd, "@id", comment.Id);
            DbUtils.AddParameter(cmd, "@body", comment.Body);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    DELETE FROM Comment
                    WHERE Comment.Id = @id";

            DbUtils.AddParameter(cmd, "@id", id);

            cmd.ExecuteNonQuery();
        }

        private Comment AssembleComment(SqlDataReader reader, Comment comment)
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
