using System;
using Core.Entity;

namespace Entity.SystemUsers
{
    public class ForgatPassword : BaseEntity
    {
        public long UserID { get; set; }

        public string Key { get; set; }

        // Relationships

        public virtual User User { get; set; }

    }
}

