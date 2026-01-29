using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{


}
public class FertilizerStockVM
{
    public int? StockID { get; set; }


    public int? FertilizerID { get; set; }

    public decimal? OpeningStock { get; set; }
    public decimal? PurchasedQty { get; set; }
    public decimal? UsedQty { get; set; }

    public string? QtyUnit { get; set; }
    public int? SocietyId { get; set; }
    public int? CompanyId { get; set; }
    public string? Remarks { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    // Dropdowns
    public List<FertilizerModel>? ddlfertilizers { get; set; }
}

public class DistributeVM
{

    public int DemandDetailsId { get; set; }


    public int FertilizerId { get; set; }


    public int? SocietyId { get; set; }

    // ================= DISPLAY (Readonly) =================

    public string? FarmerName { get; set; }
    public string? FertilizerName { get; set; }

    [Display(Name = "Total Demand")]
    public decimal FertilizerNeed { get; set; }

    [Display(Name = "Already Received")]
    public decimal ReceiveQty { get; set; }

    [Display(Name = "Pending Quantity")]
    public decimal PendingQty
        => FertilizerNeed - ReceiveQty;

    [Display(Name = "Available Stock")]
    public decimal? AvailableStock { get; set; }

    // ================= INPUT =================

    [Required]
    [Range(0.01, 999999)]
    [Display(Name = "Distribute Quantity")]
    public decimal DistributeQty { get; set; }

    [StringLength(255)]
    public string? Remarks { get; set; }
}

public class FarmerDemandReportViewModel
{
    public int FarmerId { get; set; }
    public string FarmerName { get; set; }
    public string MobileNo { get; set; }
    public string CropNameHindi { get; set; }
    public string FertilizerNameHindi { get; set; }
    public string SocietyNameHi { get; set; }
    public string NeedTime { get; set; }
    public decimal TotalCropAreaHec { get; set; }
    public string Status { get; set; }
    public decimal RecieveQty { get; set; }
    public decimal TotalFertilizerNeed { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DemandDate { get; set; }
    public DateTime? RecieveDate { get; set; }
}
public class FertilizerAddStockVM: FertilizerStockVM
{
   
    public string CompanyName { get; set; }

    
    public string SocietyName { get; set; }

    
    public string FertilizerName { get; set; }

    public int PacketType { get; set; }
    public string PacketTypeName { get; set; }

    public int PacketQty { get; set; }
    public decimal PurchasedQty { get; set; }
    public string Remarks { get; set; }
    public List<FertilizerAddStockVM> list { get; set; }

}

