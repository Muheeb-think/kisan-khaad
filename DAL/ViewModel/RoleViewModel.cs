using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalaun.Models
{
    public class RoleViewModel
    {
        public int? RoleTypeId { get; set; }
        public string RoleName { get; set; }
        [Required]
        public string RoleNameHi { get; set; }
        public bool IsActive { get; set; } = true;
        public List<RoleViewModel>? roles { get; set; }
    }
    public class UserViewModel
    {
        [Required]
        public int RoleTypeId { get; set; }
        public string? RoleName { get; set; }
        public string? Email { get; set; }
        public string? mobile { get; set; }
        [Required(ErrorMessage = "पासवर्ड आवश्यक हैं।")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "कन्फर्म पापासवर्ड आवश्यक हैं।")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "पासवर्ड और कन्फर्म पासवर्ड अलग-अलग हैं।")]
        public string ConfirmPassword { get; set; }
        public int? UserNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public List<RoleViewModel>? roles { get; set; }// for dropdown
        public List<UserViewModel> userList { get; set; } // for listing 

    }
    public class UserRolePermission 
    {
        [Required]
        public int RoleTypeId { get; set; }
        public List<RoleViewModel>? roles { get; set; }// for dropdown
        public List<UserModulesDetails> applicationModules { get; set; }

    }
    public class UserModulesDetails
    {
        public int RoleTypeId { get; set; }
        public int MainMenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool CanAccess { get; set; } = false;
        public string? MainMenuName { get; set; }
        public string? SubMenuName { get; set; }
    }
}
