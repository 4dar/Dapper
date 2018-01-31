using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using lostinthewoods.Models;
using Microsoft.Extensions.Options;


namespace lostinthewoods.Factory
{
    public class TrailFactory : IFactory<Trail>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public TrailFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }

        public void Add(Trail item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails (name, description, elevation, length, longitude, latitude) VALUES (@name, @description, @elevation, @length, @longitude, @latitude)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public List<Trail> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails").ToList();
            }
        }

        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

    }
}