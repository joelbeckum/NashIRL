using Microsoft.Data.SqlClient;
using NashIRL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tabloid.Utils;

namespace NashIRL.Repositories
{
    public class HobbyRepository : BaseRepository, IHobbyRepository
    {
        public HobbyRepository(IConfiguration config) : base(config) { }

        public List<Hobby> GetAll()
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT Id, [Name], IsApproved, ApprovedBy, ApprovedOn
                    FROM Hobby;";

            var hobbies = new List<Hobby>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var hobby = new Hobby()
                {
                    Id = DbUtils.GetIntOrZero(reader, "Id"),
                    Name = DbUtils.GetString(reader, "Name"),
                    IsApproved = DbUtils.GetBool(reader, "IsApproved"),
                    ApprovedBy = DbUtils.GetIntOrZero(reader, "ApprovedBy"),
                    ApprovedOn = DbUtils.GetNullableDateTime(reader, "ApprovedOn")
                };
                hobbies.Add(hobby);
            }
            return hobbies;
        }

        // TODO:
        // - GetById()
        // - Add()
        // - Update()
        // - Delete()
    }
}
