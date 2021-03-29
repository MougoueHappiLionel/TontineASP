using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TontineASP.Services.Entities
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
 
        public int Telephone { get; set; }

        public Utilisateur()
        {

        }

        public Utilisateur(int id, string fullname, string email, string password, int telephone)
        {
            Id = id;
            Fullname = fullname;
            Email = email;
            Password = password;
            Telephone = telephone;
        }
    }
}