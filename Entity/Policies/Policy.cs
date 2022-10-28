using System;
using Core.Entity;
using Entity.Customers;

namespace Entity.Policies
{
    public class Policy : BaseEntity
    {
        public long PolicyNo { get; set; }

        public int EndorsementNo { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? IssueDate { get; set; }

        public long CustomerId { get; set; }

        public int AgentCode { get; set; }


        // Relations
        public virtual Customer Customer { get; set; }

    }
}

