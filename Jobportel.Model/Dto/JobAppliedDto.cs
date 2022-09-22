using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Dto
{
    public class JobApplied
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
