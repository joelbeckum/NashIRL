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
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(IConfiguration config) : base(config) { }

        public List<Event> GetAll()
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT e.Id, e.[Name], e.Description, e.CreatedOn, e.EventOn, e.ImageUrl, e.UserProfileId, e.HobbyId,
	                       up.FirstName, up.LastName, up.ImageUrl AS 'UserImage',
	                       h.[Name] AS 'HobbyName'
                    FROM Event e
                    LEFT JOIN UserProfile up ON e.UserProfileId = up.Id
                    LEFT JOIN Hobby h ON e.HobbyId = h.Id
                    ORDER BY e.EventOn DESC;";

            var events = new List<Event>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Event newEvent = null;
                newEvent = AssignNewEvent(reader, newEvent);

                events.Add(newEvent);
            }
            return events;
        }

        public List<Event> GetByHobbyId(int hobbyId)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT e.Id, e.[Name], e.Description, e.CreatedOn, e.EventOn, e.ImageUrl, e.UserProfileId, e.HobbyId,
	                       up.FirstName, up.LastName, up.ImageUrl AS 'UserImage',
                    h.[Name] AS 'HobbyName'
                    FROM Event e
                    LEFT JOIN UserProfile up ON e.UserProfileId = up.Id
                    LEFT JOIN Hobby h ON e.HobbyId = h.Id
                    WHERE e.HobbyId = @hobbyId
                    ORDER BY e.EventOn DESC";

            DbUtils.AddParameter(cmd, "@hobbyId", hobbyId);

            var events = new List<Event>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Event newEvent = null;
                newEvent = AssignNewEvent(reader, newEvent);

                events.Add(newEvent);
            }
            return events;
        }

        public Event GetById(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT e.Id, e.[Name], e.Description, e.CreatedOn, e.EventOn, e.ImageUrl, e.UserProfileId, e.HobbyId,
	                       up.FirstName, up.LastName, up.ImageUrl AS 'UserImage',
                    h.[Name] AS 'HobbyName'
                    FROM Event e
                    LEFT JOIN UserProfile up ON e.UserProfileId = up.Id
                    LEFT JOIN Hobby h ON e.HobbyId = h.Id
                    WHERE e.Id = @id";

            DbUtils.AddParameter(cmd, "@id", id);

            Event newEvent = null;

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                newEvent = AssignNewEvent(reader, newEvent);
            }
            return newEvent;
        }

        public void Add(Event newEvent)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    INSERT INTO Event ([Name], Description, CreatedOn, EventOn, ImageUrl, UserProfileId, HobbyId)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @description, GETDATE(), @eventOn, @imageUrl, @userProfileId, @hobbyId)";

            DbUtils.AddParameter(cmd, "@name", newEvent.Name);
            DbUtils.AddParameter(cmd, "@description", newEvent.Description);
            DbUtils.AddParameter(cmd, "@eventOn", newEvent.EventOn);
            DbUtils.AddParameter(cmd, "@imageUrl", newEvent.ImageUrl);
            DbUtils.AddParameter(cmd, "@userProfileId", newEvent.UserProfileId);
            DbUtils.AddParameter(cmd, "@hobbyId", newEvent.HobbyId);

            newEvent.Id = (int)cmd.ExecuteScalar();
        }

        public void Update(Event newEvent)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    UPDATE Event
                    SET Name = @name,
	                    Description = @description,
	                    EventOn = @eventOn,
	                    ImageUrl = @imageUrl,
	                    HobbyId = @hobbyId
                    WHERE Event.Id = @id;";

            DbUtils.AddParameter(cmd, "@id", newEvent.Id);
            DbUtils.AddParameter(cmd, "@name", newEvent.Name);
            DbUtils.AddParameter(cmd, "@description", newEvent.Description);
            DbUtils.AddParameter(cmd, "@eventOn", newEvent.EventOn);
            DbUtils.AddParameter(cmd, "@imageUrl", newEvent.ImageUrl);
            DbUtils.AddParameter(cmd, "@hobbyId", newEvent.HobbyId);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    DELETE FROM Event
                    WHERE Event.Id = @id;";

            DbUtils.AddParameter(cmd, "@id", id);

            cmd.ExecuteNonQuery();
        }

        private Event AssignNewEvent(SqlDataReader reader, Event newEvent)
        {
            newEvent = new Event()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                Description = DbUtils.GetString(reader, "Description"),
                CreatedOn = DbUtils.GetDateTime(reader, "CreatedOn"),
                EventOn = DbUtils.GetDateTime(reader, "EventOn"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                HobbyId = DbUtils.GetInt(reader, "HobbyId")
            };
            newEvent.UserProfile = new UserProfile()
            {
                Id = DbUtils.GetInt(reader, "UserProfileId"),
                FirstName = DbUtils.GetString(reader, "FirstName"),
                LastName = DbUtils.GetString(reader, "LastName"),
                ImageUrl = DbUtils.GetString(reader, "UserImage")
            };
            newEvent.Hobby = new Hobby()
            {
                Id = DbUtils.GetInt(reader, "HobbyId"),
                Name = DbUtils.GetString(reader, "HobbyName")
            };

            return newEvent;
        }

    }
}
