using System.Collections.Generic;
using System.Configuration;
using System.Data;   
using System.Data.Common;
using System.Data.SqlClient;

namespace TontineASP.Services
{
    public class Sql
    {


        public string ConnectionString { get; private set; }

        private DbProviderFactory factory;

        public Sql(string ConStringName)
        {// creer une connection à partir du nom du provider

            ConnectionString = ConfigurationManager.
                ConnectionStrings[ConStringName].ConnectionString;
            string providerName = ConfigurationManager.
                ConnectionStrings[ConStringName].ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);//recupère un espace de nom et le passe en param

        }
        public void Execute(string commandText, IEnumerable<Parameter> parameter)
        {
            using (var con = factory.CreateConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                using (var command = factory.CreateCommand())
                {
                    command.Connection = con;
                    command.CommandText = commandText;
                    if (parameter != null)
                    {
                        foreach (var p in parameter)
                        {
                            var param = factory.CreateParameter();

                            param.ParameterName = p.Name;
                            param.Value = p.Value;
                            param.DbType = p.Type;

                            command.Parameters.Add(param);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
       }
        public DbDataReader Read(string query, IEnumerable<Parameter> parameter, bool isStoredProcedure = false)
        {
            var connection = factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            var command = factory.CreateCommand();
            command.Connection = connection;
            if(isStoredProcedure)
            {
                command.CommandType = CommandType.StoredProcedure;
            }
            command.CommandText = query;
            if (parameter != null)
            {
                foreach (var p in parameter)
                {
                    var param = factory.CreateParameter();

                    param.ParameterName = p.Name;
                    param.Value = p.Value;
                    param.DbType = p.Type;

                    command.Parameters.Add(param);
                }
            }

            return command.ExecuteReader(CommandBehavior.CloseConnection);//la fermeture d'une commande entraîne la fermeture de la connection
        }


        public class Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public DbType Type { get; set; }
            public Parameter(string name, object value, DbType type)
            {
                Name = name;
                Value = value;
                Type = type;

            }

        }
    }
   

}
