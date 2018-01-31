using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using dojoleague.Models;
using Microsoft.Extensions.Options;


namespace dojoleague.Factory
{
    public class DojoFactory : IFactory<Dojo>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public DojoFactory(IOptions<MySqlOptions> config)
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

        public void Add(Dojo dojo)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO dojos (name, location, info) VALUES (@name, @location, @info)";
                dbConnection.Open();
                dbConnection.Execute(query, dojo);
            }
        }

        public IEnumerable<Dojo> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }

        public Dojo FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos WHERE iddojo = @Id", new { Id = id }).FirstOrDefault();
            }
        }

    }
}