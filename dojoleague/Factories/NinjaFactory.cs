using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using dojoleague.Models;
using Microsoft.Extensions.Options;


namespace dojoleague.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public NinjaFactory(IOptions<MySqlOptions> config)
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

        public void Add(Ninja ninja)
        {
            using (IDbConnection dbConnection = Connection)
            {
                if (ninja.dojo == null)
                {
                    string query = "INSERT INTO ninjas(name, level, description) VALUES (@name, @level, @description)";
                    dbConnection.Open();
                    dbConnection.Execute(query, ninja);
                }
                else
                {
                    string query = "INSERT INTO ninjas(name, level, dojo_id, description) VALUES (@name, @level, @dojo_id, @description)";
                    dbConnection.Open();
                    dbConnection.Execute(query, ninja);
                }

            }
        }

        public IEnumerable<Ninja> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT dojos.name as dojo_name, dojos_id, ninjas.idninja, ninjas.name FROM ninjas LEFT JOIN dojos ON ninjas.dojos_id = dojos.id");
            }
        }

        public Ninja FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT dojos.name as dojo_name, description, ninjas.idninja, ninjas.name, ninjas.level, dojos_id FROM ninjas JOIN dojos WHERE ninjas.dojos_id = dojos.id AND ninjas.idninja = @Id", new { Id = id }).FirstOrDefault();
              
            }
        }

    }
}