using System;
using Core.Entity;
using Entity.Policies;
using static Core.Enums.Enums;
using System.Collections.Generic;

namespace Entity.Customers
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string IdentityNo { get; set; }

        //relations
        public virtual IList<Policy> Policies { get; set; }
    }
}

