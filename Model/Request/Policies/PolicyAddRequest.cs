using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Request.Policies
{
    public class PolicyAddRequest
    {
        [Required(ErrorMessage = "Must Enter PolicyNo.")]
        public long PolicyNo { get; set; }

        public int EndorsementNo { get; set; } = 0;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "IdentityNo Area Can Not Be Null")]
        [MaxLength(11, ErrorMessage = "Maximum 11 Character ")]
        [MinLength(11, ErrorMessage = "Minimum 11 Character ")]
        public string TCKN { get; set; }

        [Required(ErrorMessage = "Agent Code Area Can No Be Null.")]
        public int AgentCode { get; set; }

        public long? ParentPolicyId { get; set; }
    }
}
