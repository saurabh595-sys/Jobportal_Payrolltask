using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Model.Dto.JobDto
{
    public class GetJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public  string Description { get; set; }
        public string CreatedByName { get; set; }
        public DateTime JobEndAt { get; set; }
        public bool IsActive { get; set; }
    }
}
