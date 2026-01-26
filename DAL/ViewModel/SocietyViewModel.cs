using DAL.ViewModel;
using Jalaun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalaun.Models
{
    
    public class SocietyViewModel
    {
        public int? Id { get; set; }
        public string? SocietyNameEng { get; set; }
        public string SocietyNameHi { get; set; }
        public int TehsilId { get; set; }
        public int VillageId { get; set; }
        public int BlockId { get; set; }
        public int? DistrictId { get; set; }
        public string? District { get; set; }
        public string? Tehsil { get; set; }
        public string? Block { get; set; }
        public string? Village { get; set; }
        public long? Mobile { get; set; }
        public string? Email { get; set; }
        public int? CreatedBy { get; set; }
        public List<TehsilViewModel>? ddltehsil { get; set; }
        public List<BlockModel>? ddlblock { get; set; }
        public List<VillageModel>? ddlvillage { get; set; }
    }


}
