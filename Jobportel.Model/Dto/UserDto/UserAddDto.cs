using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Model.Dto.UserDto
{
    public class UserAddDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
