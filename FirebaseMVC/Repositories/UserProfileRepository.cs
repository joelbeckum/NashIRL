using Microsoft.Data.SqlClient;
using NashIRL.Models;
using Microsoft.Extensions.Configuration;
using Tabloid.Utils;

namespace NashIRL.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config) { }

        public UserProfile GetById(int id)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT Id, FirebaseUserId, FirstName, LastName, Email, UserTypeId, ImageUrl
                    FROM UserProfile
                    WHERE Id = @id";

            DbUtils.AddParameter(cmd, "@id", id);

            UserProfile userProfile = null;

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                userProfile = AssembleUserProfile(reader, userProfile);
            }

            return userProfile;
        }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                    SELECT Id, FirebaseUserId, FirstName, LastName, Email, UserTypeId, ImageUrl
                    FROM UserProfile
                    WHERE FirebaseUserId = @firebaseUserId";

            DbUtils.AddParameter(cmd, "@firebaseUserId", firebaseUserId);

            UserProfile userProfile = null;

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                userProfile = AssembleUserProfile(reader, userProfile);
            }

            return userProfile;
        }

        public void Add(UserProfile userProfile)
        {
            using var conn = Connection;
            conn.Open();
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = @"
                    INSERT INTO
                        UserProfile (FirebaseUserId, FirstName, LastName, Email, UserTypeId) 
                    OUTPUT INSERTED.ID
                    VALUES(@firebaseUserId, @firstName, @lastName, @email, @userTypeId)";

            DbUtils.AddParameter(cmd, "@firebaseUserId", userProfile.FirebaseUserId);
            DbUtils.AddParameter(cmd, "@firstName", userProfile.FirstName);
            DbUtils.AddParameter(cmd, "@lastName", userProfile.LastName);
            DbUtils.AddParameter(cmd, "@email", userProfile.Email);
            DbUtils.AddParameter(cmd, "@userTypeId", userProfile.UserTypeId);

            userProfile.Id = (int)cmd.ExecuteScalar();
        }

        private UserProfile AssembleUserProfile(SqlDataReader reader, UserProfile userProfile)
        {
            userProfile = new UserProfile()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                FirstName = DbUtils.GetString(reader, "FirstName"),
                LastName = DbUtils.GetString(reader, "LastName"),
                Email = DbUtils.GetString(reader, "Email"),
                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl")
            };

            return userProfile;
        }
    }
}
