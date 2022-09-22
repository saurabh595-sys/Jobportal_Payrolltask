using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jobportel.Data.Model
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public int  jobId { get; set; }
        public int AppliedBy { get; set; }
        public DateTime AppliedAt { get; set; }
        public bool isActive { get; set; }
    }
}
