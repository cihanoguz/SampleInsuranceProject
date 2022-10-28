using System;
using Core.Entity;

namespace Entity.Customers
{
    public class Agent : BaseEntity
    {
        public int AgentCode { get; set; }

        public string Name { get; set; }

        public int BranchCode { get; set; }

        public int CompanyCode { get; set; }
    }
}

