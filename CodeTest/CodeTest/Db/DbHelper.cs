using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CodeTest.Db
{
    public class DbHelper<T> where T : class
    {
        private readonly string _mConnectionString;
        private readonly string _mQuery;
        private readonly IDictionary<string, object> _mParameters;

        public Func<SqlDataReader, T> Mapper { get; set; }

        public DbHelper(string mQuery)
        {
            _mConnectionString = ConfigurationManager.ConnectionStrings["CodeTestConnection"].ToString();
            _mQuery = mQuery;
            _mParameters = new Dictionary<string, object>();
        }

        public void AddParameter(string name, object value)
        {
            _mParameters.Add(name, value);
        }

        // multi-purpose get that applies sql parameters, and invokes mapper function to map results
        // to object (T) as needed
        public List<T> Get()
        {
            var results = new List<T>();

            using (var connection = new SqlConnection(_mConnectionString))
            {
                using (var command = new SqlCommand(_mQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Connection.Open();

                    foreach (var parameter in _mParameters)
                    {
                        var p = new SqlParameter() { ParameterName = parameter.Key, Value = parameter.Value };
                        command.Parameters.Add(p);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(Mapper(reader));
                        }
                    }
                }
            }
            return results;
        }
    }
}