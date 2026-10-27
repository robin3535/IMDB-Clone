using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ImdbApi.Models.ResponseModel;
using Microsoft.Data.SqlClient;

namespace ImdbApi.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<T> Get(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query);
        }
        public T Get(string query,object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QueryFirstOrDefault<T>(query,parameters);
        }
        public IEnumerable<T> Gets(string query, object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<T>(query, parameters);
        }
        public int Create(string spName,object parameter)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingle<int>(spName, parameter,commandType: CommandType.StoredProcedure);
        }
        public void Update(string spName, object parameter)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(spName, parameter, commandType: CommandType.StoredProcedure);
        }
        public void Delete(string spName,object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Query(spName, parameters,commandType: CommandType.StoredProcedure);
        }
        public void UpdatePatch(string query,object parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(query, parameters);
        }
    }
}
