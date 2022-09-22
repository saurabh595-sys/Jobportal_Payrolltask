using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Model.Dto.UserDto
{
   public class UserGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
