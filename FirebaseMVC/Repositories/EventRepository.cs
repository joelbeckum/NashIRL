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
                    LEFT JOIN Hobby h ON e.HobbyId = h.Id;";

            var events = new List<Event>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var newEvent = new Event()
                {
                    Id = DbUtils.GetIntOrZero(reader, "Id"),
                    Name = DbUtils.GetString(reader, "Name"),
                    Description = DbUtils.GetString(reader, "Description"),
                    CreatedOn = DbUtils.GetDateTime(reader, "CreatedOn"),
                    EventOn = DbUtils.GetDateTime(reader, "EventOn"),
                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                    UserProfileId = DbUtils.GetIntOrZero(reader, "UserProfileId"),
                    HobbyId = DbUtils.GetIntOrZero(reader, "HobbyId")
                };
                newEvent.UserProfile = new UserProfile()
                {
                    Id = DbUtils.GetIntOrZero(reader, "UserProfileId"),
                    FirstName = DbUtils.GetString(reader, "FirstName"),
                    LastName = DbUtils.GetString(reader, "LastName"),
                    ImageUrl = DbUtils.GetString(reader, "UserImage")
                };
                newEvent.Hobby = new Hobby()
                {
                    Id = DbUtils.GetIntOrZero(reader, "HobbyId"),
                    Name = DbUtils.GetString(reader, "HobbyName")
                };

                events.Add(newEvent);
            }
            return events;
        }

        // TODO:
        // - GetById()
        // - Add()
        // - Update()
        // - Delete()
    }
}
