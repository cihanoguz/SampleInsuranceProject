using System;
using Core.Mapping;
using Entity.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mapping.Customers
{
    public class AgentMapping : BaseMap<Agent>
    {
        public AgentMapping(EntityTypeBuilder<Agent> builder)
        {

        }
    }
}

