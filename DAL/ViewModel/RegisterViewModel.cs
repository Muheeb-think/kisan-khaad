using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string FatherHusbandName { get; set; }
        public string FormerCategory { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string tahsil { get; set; }
        public string AreaType { get; set; }
        public string village { get; set; }
        public string pincode { get; set; }
        public string address { get; set; }
        public string LandRecordNumber { get; set; }
        public string TotalArea { get; set; }
        public string TotalAreaAgristack { get; set; }
        public string FarmerShare { get; set; }
        public string AreaofPaddySown { get; set; }
        public DateOnly? DOB { get; set; }
        public List<TehsilModel> TehsilList { get; set; }
        public List<FarmerCategoryModel> FarmerCategoryList { get; set; }
        public List<BlockModel> BlockList { get; set; }
        public List<VillageModel> VillageList { get; set; }
    }
    public class TehsilModel
    {
        public int Id { get; set; }
        public string? TehsilName { get; set; }
    }

    public class FarmerCategoryModel
    {
        public int CategoryId { get; set; }
        public string? CategoryNameHindi { get; set; }
    }

    public class BlockModel
    {
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
    }

    public class VillageModel
    {
        public int VillageId { get; set; }
        public string? VillageName { get; set; }
    }

}
