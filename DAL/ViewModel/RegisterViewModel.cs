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
        [Required(ErrorMessage = "किसान आईडी भरे")]
        public string? FarmerId { get; set; }
        [Required]
        [Display(Name = "Farmer Name")]
        public string? FarmerName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? AadharCardNo { get; set; }

        [Required(ErrorMessage = "पासवर्ड आवश्यक है")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "कन्फर्म पासवर्ड आवश्यक है")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "पासवर्ड और कन्फर्म पासवर्ड समान नहीं हैं")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        public string? MobileNumberAgriStack { get; set; }
        public string? FatherHusbandName { get; set; }
        public string? FormerCategory { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? tahsilId { get; set; }
        public string? LandState { get; set; }
        public string? LandDistrict { get; set; }
        public string? Landtahsil { get; set; }
        public string? LandVillage { get; set; }
        public string? AreaType { get; set; }
        public string? village { get; set; }
        public string? pincode { get; set; }
        public string? pincodeAgriStack { get; set; }
        public string? tahsilAgriStack { get; set; }
        public string? address { get; set; }
        public string? LandRecordNumber { get; set; }
        public string? LandTotalArea { get; set; }
        public int FarmerCategoryId { get; set; }
        public string? TotalAreaAgristack { get; set; }
        public string? FarmerShare { get; set; }
        public string? AreaofPaddySown { get; set; }
        public string? MobileNumberAgriStackForLand { get; set; }
        public string? FarmerNameForLand { get; set; }
        public string? FarmerNameLandKhasra { get; set; }
        public DateOnly? DOB { get; set; }

        public List<TehsilModel>? TehsilList { get; set; }
        public List<FarmerCategoryModel>? FarmerCategoryList { get; set; }
        public List<BlockModel>? BlockList { get; set; }
        public List<Village>? VillageList { get; set; }
        public List<LandViewModel> LandList { get; set; }
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
    public class FarmerSearchRequest
    {
        public string Action { get; set; }
        public string FarmerId { get; set; }
    }
    public class FertilizerDetails
    {
        public string Action { get; set; }
        public string CropId { get; set; }
    }
    public class LandViewModel
    {
        public int DistrictId { get; set; }
        public int TahsilId { get; set; }
        public int VillageId { get; set; }
        public string LandRecordNumber { get; set; }
        public decimal LandTotalArea { get; set; }
        public decimal FarmerShare { get; set; }
    }

 //From: Mukeem(THINKCLIENTPF1T/192.168.2.12/THINKCLIENTSEQRITE-<84733e427e285401>)
 // at Sat Jan 24 10:54:20 2026
public class TempraryDemand
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string FarmerId { get; set; }
        public int SamitiId { get; set; }
        public int SeasonId { get; set; }
        public int CropId { get; set; }
        public int FertilizerId { get; set; }
        public int NeedTimeId { get; set; }
        public decimal CropAreaHec { get; set; }
        public decimal FertilizerNeed { get; set; }
        public string? SpecialInstraction { get; set; }
    }
    public class TempDemandVM
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public string? msg { get; set; }
        public string? CropNameHindi { get; set; }
        public string? FertilizerNameHindi { get; set; }
        public decimal FertilizerNeed { get; set; }
        public decimal CropAreaHec { get; set; }
        public decimal Rate_Kg { get; set; }
        public decimal Amount { get; set; }
    }
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }


}
