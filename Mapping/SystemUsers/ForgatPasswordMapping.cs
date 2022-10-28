using System;
using Core.Mapping;
using Entity.SystemUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mapping.SystemUsers
{
    public class ForgatPasswordMapping : BaseMap<ForgatPassword>
    {
        public ForgatPasswordMapping(EntityTypeBuilder<ForgatPassword> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.ForgatPasswords).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

