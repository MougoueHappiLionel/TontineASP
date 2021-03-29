using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TontineASP.Services.Entities;

namespace TontineASP.Services
{
    public class Authentication
    {
        public AuthenficateCommand Command { get; private set; }

        public Authentication(AuthenficateCommand command)
        {
            Command = command;
        }
        public Utilisateur Execute()
        {
            Sql sql = new Sql("SqlServer");
            var reader = sql.Read("Sp_Authentication_Users", new Sql.Parameter[]
            {
                new Sql.Parameter("@Email", Command.Email, System.Data.DbType.String),
                 new Sql.Parameter("@Password", Command.Password, System.Data.DbType.String),
            }, true);

            if (reader != null)
            {
                while (reader.Read())
                {
                    return new Utilisateur
                    (
                        int.Parse(reader["Id"].ToString()),
                         reader["Fullname"].ToString(),
                        reader["Email"].ToString(),
                        reader["Password"].ToString(),
                        int.Parse(reader["Telephone"].ToString())
                    );
                }
                reader.Close();
            }
            return null;
        }
    }

    public class AuthenficateCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenficateCommand()
        {

        }

        public AuthenficateCommand(string email, string password)
        {
            Email = email;
            Password = password;
  
        }
    }
}