using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TontineASP.Models
{
    public class RegisterModel
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email adress")]
        public string Email { get; set; }

        [Required]
        public int Telephone { get; set; }

        public bool IsError { get; set; } = false;

        public string Message { get; set; }
    }
}