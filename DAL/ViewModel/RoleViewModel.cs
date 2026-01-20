using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalaun.Models
{
    public class RoleViewModel
    {
        public int? RoleTypeId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameHi { get; set; }
        public bool IsActive { get; set; } = true;
        public List<RoleViewModel>? roles { get; set; }
    }
    public class UserViewModel
    {
        public int RoleTypeId { get; set; }
        public string? Email { get; set; }
        public string? mobile { get; set; }
        public int Password { get; set; }
        public int ConfirmPassword { get; set; }
        public int? UserNumber { get; set; }
        public bool? IsActive { get; set; } = true;
        public List<RoleViewModel>? roles { get; set; }

    }
}
