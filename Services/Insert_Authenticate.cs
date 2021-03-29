using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TontineASP.Services.Entities;

namespace TontineASP.Services
{
    public class Insert_Authenticate
    {
        public AuthenticateInsert Command { get; private set; }

        public  Insert_Authenticate(AuthenticateInsert command)
        {
            Command = command;
        }
        public Utilisateur Execute()
        {
            Sql sql = new Sql("SqlServer");
            var reader = sql.Read("Sp_Authentication_Insert", new Sql.Parameter[]
            {
                new Sql.Parameter("@Fullname", Command.Fullname, System.Data.DbType.String),
                new Sql.Parameter("@Email", Command.Email, System.Data.DbType.String),
                 new Sql.Parameter("@Password", Command.Password, System.Data.DbType.String),
                new Sql.Parameter("@Telephone", Command.Telephone, System.Data.DbType.String)

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

    public class AuthenticateInsert
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int Telephone { get; set; }

        public AuthenticateInsert()
        {

        }

        public AuthenticateInsert(string fullname, string email, string password, int telephone)
        {
            Fullname = fullname;
            Email = email;
            Password = password;
            Telephone = telephone;

        }
    }
}