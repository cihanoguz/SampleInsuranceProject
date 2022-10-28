using System;
using Core.Mapping;
using Entity.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mapping.Customers
{
    public class CustomerMapping : BaseMap<Customer>
    {
        public CustomerMapping(EntityTypeBuilder<Customer> builder)
        {
          
        }
    }
}


