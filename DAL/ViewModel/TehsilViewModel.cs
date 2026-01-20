

using DAL.ViewModel;
using System.Text.Json.Serialization;

namespace Jalaun.Models
{
    public class TehsilViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? TehsilNameEng { get; set; }
        public string TehsilNameHi { get; set; }
        public string? DistrictName { get; set; }
        public int? BlockId { get; set; }
        public List<VillageModel>? ddlvillage { get; set; }

    }


}

