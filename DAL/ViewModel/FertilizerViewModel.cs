using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   

}
public class FertilizerStockVM
{
    public int StockID { get; set; }

    [Required]
    public int FertilizerID { get; set; }

    public decimal? OpeningStock { get; set; }
    public decimal? PurchasedQty { get; set; }
    public decimal? UsedQty { get; set; }

    public string QtyUnit { get; set; }
    public int? SocietyId { get; set; }

    public string Remarks { get; set; }

    // Dropdowns
    public List<SelectListItem> FertilizerList { get; set; }
}

public class DistributeVM
{

    public int DemandDetailsId { get; set; }


    public int FertilizerId { get; set; }


    public int SocietyId { get; set; }

    // ================= DISPLAY (Readonly) =================

    public string FarmerName { get; set; }
    public string FertilizerName { get; set; }

    [Display(Name = "Total Demand")]
    public decimal FertilizerNeed { get; set; }

    [Display(Name = "Already Received")]
    public decimal ReceiveQty { get; set; }

    [Display(Name = "Pending Quantity")]
    public decimal PendingQty
        => FertilizerNeed - ReceiveQty;

    [Display(Name = "Available Stock")]
    public decimal AvailableStock { get; set; }

    // ================= INPUT =================

    [Required]
    [Range(0.01, 999999)]
    [Display(Name = "Distribute Quantity")]
    public decimal DistributeQty { get; set; }

    [StringLength(255)]
    public string Remarks { get; set; }
}

