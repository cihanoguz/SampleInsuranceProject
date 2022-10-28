using Entity.Customers;
using System;
using static Core.Enums.Enums;

namespace Model.Response.Customers
{
    public class CustomerResponse
    {
        public CustomerResponse()
        {

        }

        public CustomerResponse(Customer customer)
        {
            Name = customer.Name;
            Surname = customer.Surname;
            BirthDate = customer.BirthDate;
            Gender = customer.Gender;
            IdentityNo = customer.IdentityNo;
            //CustomerId = customer.Id;
            CreatedDate = customer.CreateDate;
        }

        public Guid CustomerId { get; set; }
 
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string IdentityNo { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
