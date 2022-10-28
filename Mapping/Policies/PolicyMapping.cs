using System;
using Core.Mapping;
using Entity.Policies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mapping.Policies
{
    public class PolicyMapping : BaseMap<Policy>
    {
        public PolicyMapping(EntityTypeBuilder<Policy> builder)
        {
            builder.HasOne(x => x.Customer).WithMany(x => x.Policies).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

