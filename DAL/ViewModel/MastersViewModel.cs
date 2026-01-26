using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class VillageViewMaster
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ब्लॉक चुनें")]
        public int? BlockId { get; set; }
        [Required(ErrorMessage = "गांव का नाम दर्ज करें")]
        [StringLength(50, ErrorMessage = "गांव का नाम 50 अक्षरों से अधिक नहीं हो सकता")]
        public string? VillageNameHindi { get; set; }
        public string? VillageNameEnglish { get; set; }
        public List<Village>? Villages { get; set; }
        public List<BlockModel>? BlockList { get; set; }
    }
    public class Village
    {
        public int Id { get; set; }
        public int? BlockId { get; set; }
        public string? BlockName { get; set; }
        public string? VillageNameHi { get; set; }
        public string? VillageNameEn { get; set; }
    }

    public class CropViewModel
    {
        public int CropId { get; set; }
        [Required(ErrorMessage = "फसल चुनें")]
        public int? SeasonId { get; set; }

        [Required(ErrorMessage = "फसल का नाम दर्ज करें")]
        [StringLength(50, ErrorMessage = "फसल का नाम 50 अक्षरों से अधिक नहीं हो सकता")]
        public string? CropNameHindi { get; set; }
        public List<SeasonModel>? seasonModels { get; set; }
        public List<CropModel>? CropModels { get; set; }
    }
    public class CropModel
    {
        public int CropId { get; set; }
        public string? CropNameHindi { get; set; }
        public string? SeasonNameHindi { get; set; }
        public int SeasonId { get; set; }
    }
    public class SeasonModel
    {
        public string? SeasonNameHindi { get; set; }
        public int SeasonId { get; set; }
    }

    public class FertilizerViewModel
    {
        public int FertilizerId { get; set; }

        [Required(ErrorMessage = "उर्वरक का नाम दर्ज करें")]
        [StringLength(50, ErrorMessage = "उर्वरक का नाम 50 अक्षरों से अधिक नहीं हो सकता")]
        public string? FertilizerNameHindi { get; set; }
        public string? FertilizerNameEnglish { get; set; }
        [Required(ErrorMessage = "उर्वरक का दर दर्ज करें")]
        public string? FertilizerRate { get; set; }
        public int CropId { get; set; }
        public List<CropModel>? CropModels { get; set; }
        public List<FertilizerModel>? Fertilizers { get; set; }
    }
    public class FertilizerModel
    {
        public int FertilizerId { get; set; }
        public string? FertilizerNameHindi { get; set; }
        public string? FertilizerNameEnglish { get; set; }
        public string? FertilizerRate { get; set; }
    }

    public class FertilizerCropMappingViewModel
    {
        public int MappingId { get; set; }
        [Required(ErrorMessage = "उर्वरक चुनें")]
        public int FertilizerId { get; set; }
        [Required(ErrorMessage = "फसल चुनें")]
        public int CropId { get; set; }
        public string? Dose_per_hectare_kg { get; set; }
        public List<CropModel>? CropModels { get; set; }

        public List<FertilizerModel>? Fertilizers { get; set; }

        public List<FertilizerCropMappingModel>? FertilizerCropMappings { get; set; }
    }

   
   

    public class FertilizerCropMappingModel
    {

        public int MappingId { get; set; }
        public int? CropId { get; set; }
        public string? CropNameHindi { get; set; }
        public int FertilizerId { get; set; }
        public string? FertilizerNameHindi { get; set; }
        public string? FertilizerNameEnglish { get; set; }
        public string? Dose_per_hectare_kg { get; set; }
    }
    public class FertilizerDemandVM
    {
        public DataTable DashboardDT { get; set; }
        public string ItemName { get; set; }
        public int DemandKg { get; set; }
       public List<FertilizerDemandVM>? FertilizerDemandVillageWise { get; set; }
        public List<FertilizerDemandVM>? FertilizerDemandFertilizerWise { get; set; }
    }

}
