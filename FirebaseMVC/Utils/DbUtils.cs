using System;
using Microsoft.Data.SqlClient;

namespace Tabloid.Utils
{
    public static class DbUtils
    {
        //  A utility class with static methods for more gracefully handling adding and extracting values in Repository methods, including correctly parsing NULL values
        public static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetString(ordinal);
        }

        //  Get an int from a data reader object
        //  This method assumes the value is not NULL
        public static int GetIntOrZero(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }
            return reader.GetInt32(ordinal);
        }

        //  Get a DateTime from a data reader object
        //  This method assumes the value is not NULL
        public static DateTime GetDateTime(SqlDataReader reader, string column)
        {
            return reader.GetDateTime(reader.GetOrdinal(column));
        }

        //  Get a nullable DateTime from a data reader object
        public static DateTime? GetNullableDateTime(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetDateTime(ordinal);
        }

        public static bool GetBool(SqlDataReader reader, string column)
        {
            return reader.GetBoolean(reader.GetOrdinal(column));

        }

        //  Determine if the value a given column is NULL
        public static bool IsDbNull(SqlDataReader reader, string column)
        {
            return reader.IsDBNull(reader.GetOrdinal(column));
        }

        //  Determine if the value a given column is not NULL
        public static bool IsNotDbNull(SqlDataReader reader, string column)
        {
            return !IsDbNull(reader, column);
        }

        //  Add a parameter to the given SqlCommand object - if parameter is NULL, convert to DBNull
        public static void AddParameter(SqlCommand cmd, string name, object value)
        {
            if (value == null)
            {
                cmd.Parameters.AddWithValue(name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(name, value);
            }
        }
    }
}
