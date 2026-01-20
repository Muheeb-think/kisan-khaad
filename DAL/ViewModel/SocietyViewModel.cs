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
        public string? SocietyNameHi { get; set; }
        public int TehsilId { get; set; }
        public int VillageId { get; set; }
        public string? VillageName { get; set; }
        public string? TehsilName { get; set; }
        public List<TehsilViewModel>? ddltehsil { get; set; }
        public List<BlockModel>? ddlblock { get; set; }
        public List<VillageModel>? ddlvillage { get; set; }
    }


}
