using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TontineASP.Models
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsError { get; set; } = false;

        public string Message { get; set; }
    }
}