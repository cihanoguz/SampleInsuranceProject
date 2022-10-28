using System;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Model.Request.Customers
{
    public class CustomerAddRequest
    {

        [Required(ErrorMessage = "Name Area Can Not Be Null.")]
        [MinLength(2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname Area Can Not Be Null")]
        [MinLength(2)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Birthday Area Can Not Be Null")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Gender Area Can Not Be Null")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "IdentityNo Area Can Not Be Null")]
        [MaxLength(11, ErrorMessage = "Maximum 11 Character ")]
        [MinLength(11, ErrorMessage = "Minimum 11 Character ")]
        public string IdentityNo { get; set; }
 
    }
}
