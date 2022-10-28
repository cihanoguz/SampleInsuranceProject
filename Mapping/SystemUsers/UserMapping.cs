using System;
using Core.Mapping;
using Entity.SystemUsers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mapping.SystemUsers
{
    public class UserMapping : BaseMap<User>
    {
        public UserMapping(EntityTypeBuilder<User> builder)
        {

        }
    }
}

