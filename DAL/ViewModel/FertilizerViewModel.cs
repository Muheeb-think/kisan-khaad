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
