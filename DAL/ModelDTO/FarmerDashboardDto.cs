using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelDTO
{
    public class FarmerDashboardDto
    {
        public int FarmerId { get; set; }
        public decimal TotalCropAreaHec { get; set; }
        public decimal TotalFertilizerNeed { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCrops { get; set; }
        public decimal SuccessDemand { get; set; }
        public string? SoilQuality { get; set; }
        public decimal FertilizerNeed { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal TotalDueDemand { get; set; }
        public List<FertilizerChartVM>? fertilizerCharts { get; set; }
    }


    public class FertilizerChartVM
    {
        public int OrderYear { get; set; }
        public int OrderMonthNo { get; set; }
        public string? OrderMonthHindi { get; set; }
        public decimal FertilizerDemand { get; set; }
        public int TotalOrders { get; set; }
    }

}
