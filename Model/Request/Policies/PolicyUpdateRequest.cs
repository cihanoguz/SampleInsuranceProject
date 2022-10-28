using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Request.Policies
{
    public class PolicyUpdateRequest
    {
        public long PolicyId { get; set; }
        public int EndorsementNo { get; set; } 

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "Agent Code Area Can No Be Null.")]
        public string AgentCode { get; set; }

        public long? ParentPolicyId { get; set; }
    }
}
