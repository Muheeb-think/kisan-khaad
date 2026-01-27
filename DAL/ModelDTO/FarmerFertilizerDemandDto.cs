using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelDTO
{
    public class FarmerFertilizerDemandDto
    {
        public string? FarmerId { get; set; }
        public string? FarmerName { get; set; }
        public decimal MobileNo { get; set; }
        public string? CropNameHindi { get; set; }
        public string? FertilizerNameHindi { get; set; }
        public string? SocietyNameHi { get; set; }
        public string? NeedTime { get; set; }
        public decimal TotalCropAreaHec { get; set; }
        public string? Status { get; set; }
        public decimal TotalFertilizerNeed { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCrops { get; set; }
        public decimal SuccessDemand { get; set; }
        public decimal RecieveQty { get; set; }
        public DateTime DemandDate { get; set; }
    }
}
