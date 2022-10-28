using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Request.Customers
{
    public class AgentAddRequest
    {
        [Required(ErrorMessage ="Must Enter Agent Code.")]
        public int AgentCode { get; set; }

        [Required(ErrorMessage = "Must Enter Agent Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must Enter Region Code.")]
        public int BranchCode { get; set; }

        [Required(ErrorMessage = "Must Enter Tax Number.")]
        public int CompanyCode { get; set; }
    }
}
