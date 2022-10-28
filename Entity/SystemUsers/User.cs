using System;
using Core.Entity;
using static Core.Enums.Enums;
using System.Collections.Generic;

namespace Entity.SystemUsers
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        //Relations 
        public virtual IList<ForgatPassword> ForgatPasswords { get; set; }
    }
}

