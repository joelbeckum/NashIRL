using Microsoft.Data.SqlClient;
using NashIRL.Models;
using System.Collections.Generic;
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
                Hobby hobby = null;
                hobby = AssembleHobby(reader, hobby);
                hobbies.Add(hobby);
            }
            return hobbies;
        }

        public Hobby GetById(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT Id, [Name], IsApproved, ApprovedBy, ApprovedOn
                    FROM Hobby
                    WHERE Hobby.Id = @id;";

            DbUtils.AddParameter(cmd, "@id", id);

            Hobby hobby = null;

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                hobby = AssembleHobby(reader, hobby);
            }

            return hobby;
        }

        public void Add(Hobby hobby)
        {
            var conn = Connection;
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    INSERT INTO Hobby (Name, IsApproved)
                    OUTPUT INSERTED.Id
                    VALUES (@name, 0)";

            DbUtils.AddParameter(cmd, "@name", hobby.Name);

            hobby.Id = (int)cmd.ExecuteScalar();
        }

        public void Approve(int id, int adminId)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    UPDATE Hobby
                    SET IsApproved = 1,
	                    ApprovedBy = @adminId,
	                    ApprovedOn = GETDATE()
                    WHERE Hobby.Id = @id";

            DbUtils.AddParameter(cmd, "@id", id);
            DbUtils.AddParameter(cmd, "@adminId", adminId);

            cmd.ExecuteNonQuery();
        }

        public void Update(Hobby hobby)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    UPDATE Hobby
                    SET [Name] = @name
                    WHERE Hobby.Id = @id";

            DbUtils.AddParameter(cmd, "@name", hobby.Name);
            DbUtils.AddParameter(cmd, "@id", hobby.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = Connection;
            conn.Open();
            var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    DELETE FROM Hobby
                    WHERE Hobby.Id = @id";

            DbUtils.AddParameter(cmd, @"id", id);

            cmd.ExecuteNonQuery();
        }

        private Hobby AssembleHobby(SqlDataReader reader, Hobby newHobby)
        {
            newHobby = new Hobby()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                IsApproved = DbUtils.GetBool(reader, "IsApproved"),
                ApprovedBy = DbUtils.GetNullableInt(reader, "ApprovedBy"),
                ApprovedOn = DbUtils.GetNullableDateTime(reader, "ApprovedOn")
            };

            return newHobby;
        }
    }
}
