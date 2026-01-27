using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class FarmerProfile
    {
        public int Correspondence_TehsilId { get; set; }
        public int Correspondence_BlockId { get; set; }
        public string? Correspondence_Pincode { get; set; }
        public int Correspondence_VillageId { get; set; }
        public string? Correspondence_MobileNo { get; set; }
        public string? Correspondence_FullAddresss { get; set; }
        public int FarmerId { get; set; }
        public string? AadharNo { get; set; }

    }
    public class UserProfileViewModel
    {
        [Required]
        public string? State { get; set; }

        public FarmerProfile Farmer { get; set; }

        public List<TehsilModel>? TehsilList { get; set; }

        public List<VillageModel>? VillageList { get; set; }

        [Required]
        [MaxLength(6)]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "पिनकोड 6 अंकों का होना चाहिए।")]
        public string? Correspondence_Pincode { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "मोबाइल नंबर 10 अंकों का होना चाहिए।")]
        public string? Correspondence_MobileNo { get; set; }
        public int Correspondence_TehsilId { get; set; }
        public int Correspondence_BlockId { get; set; }
        public int Correspondence_DistrictId { get; set; }
        public int Correspondence_VillageId { get; set; }
        public string? Correspondence_FullAddresss { get; set; }
        public int FarmerId { get; set; }
        public string? AadharNo { get; set; }
        public string? Action { get; set; }

    }

    public class ForgetPassword()
    {
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "पासवर्ड कम से कम 8 अक्षरों का होना चाहिए।")]
        public string? OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "पासवर्ड कम से कम 8 अक्षरों का होना चाहिए।")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "पासवर्ड और कन्फर्म पासवर्ड मेल नहीं खाते।")]
        public string NewConfirmPassword { get; set; }
    }
}
