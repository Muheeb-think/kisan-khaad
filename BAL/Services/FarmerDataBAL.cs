using BAL.Common;
using DAL.ModelDTO;
using DAL.Repo;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BAL.Services
{
    public interface IFarmerDataBAL
    {
        public List<FarmerProfile> SelectCorrespondenceFarmerData();
        public int UpdateCorrespondenceFarmerData(UserProfileViewModel obj);
        public FarmerDashboardDto GetFarmerDashboard();
       public List<FarmerFertilizerDemandDto> GetFarmerFertilizerDemand(string? Action);
    }
    public class FarmerDataBal : IFarmerDataBAL
    {
        private readonly IFarmerDataDAL _farmerdal;
        public FarmerDataBal(IFarmerDataDAL farmerDataDAL)
        {
            this._farmerdal= farmerDataDAL;    
        }
        public List<FarmerProfile> SelectCorrespondenceFarmerData()
        {
            DataTable dt = _farmerdal.GetCorrespondenceFarmerData(SessionHelper.UserMobile);
            List<FarmerProfile> profiles = new();

            foreach (DataRow row in dt.Rows)
            {
                profiles.Add(new FarmerProfile
                {
                    FarmerId = Convert.ToInt32(row["FarmerId"]),
                    Correspondence_FullAddresss = row["Correspondence_FullAddresss"].ToString(),
                    Correspondence_MobileNo = row["Correspondence_MobileNo"].ToString(),
                    Correspondence_VillageId = Convert.ToInt32(row["Correspondence_VillageId"].ToString()),
                    Correspondence_Pincode = row["Correspondence_Pincode"].ToString(),
                   // Correspondence_BlockId = Convert.ToInt32(row["Correspondence_BlockId"].ToString()),
                    Correspondence_TehsilId = Convert.ToInt32(row["Correspondence_TehsilId"].ToString()),
                });
            }
            return profiles;
        }

        public FarmerDashboardDto GetFarmerDashboard()
        {
            DataSet ds = _farmerdal.FarmerDasBoardData(SessionHelper.UserMobile);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            FarmerDashboardDto dashboard = new FarmerDashboardDto
            {
                FarmerId = row.Field<int>("FarmerId"),
                TotalCropAreaHec = row.Field<decimal>("TotalCropAreaHec"),
                TotalFertilizerNeed = row.Field<decimal>("TotalFertilizerNeed"),
                TotalAmount = row.Field<decimal>("TotalAmount"),
                TotalCrops = row.Field<int>("TotalCrops"),
                SuccessDemand = row.Field<decimal>("SuccessDemand"),
                SoilQuality = row.Field<string>("SoilQuality"),
                TotalAmountDue = row.Field<decimal>("TotalAmountDue"),
                FertilizerNeed = row.Field<decimal>("FertilizerNeed"),
                TotalDueDemand = row.Field<decimal>("TotalDueDemand"),
                fertilizerCharts = new List<FertilizerChartVM>()
            };

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                dashboard.fertilizerCharts = ds.Tables[1]
                    .AsEnumerable()
                    .Select(r => new FertilizerChartVM
                    {
                        FertilizerNameHindi = r.Field<string?>("FertilizerNameHindi"),
                        TotalNeedFertilizer = r.Field<decimal>("TotalNeedFertilizer")

                    })
                    .ToList();
            }

            return dashboard;
        }


        public int UpdateCorrespondenceFarmerData(UserProfileViewModel obj)
        { 
            int result = _farmerdal.UpdateCorrespondenceFarmerData(obj);
            return result;
        }

        public List<FarmerFertilizerDemandDto> GetFarmerFertilizerDemand(string? Action)
        {
            DataTable dt = _farmerdal.GetFarmerFertilizerDemand(Action,SessionHelper.UserMobile);
            if (dt.Rows.Count == 0)
                return new List<FarmerFertilizerDemandDto>();
            if (dt.Rows[0][1].ToString() == "-99")
                return new List<FarmerFertilizerDemandDto>();
            List<FarmerFertilizerDemandDto> result = new List<FarmerFertilizerDemandDto>();

            foreach (DataRow row in dt.Rows)
            {
                result.Add(new FarmerFertilizerDemandDto
                {
                    FarmerId = row.Field<string>("FarmerId"),
                    FarmerName = row.Field<string?>("FarmerName"),
                    MobileNo = row.Field<decimal>("MobileNo"),
                    CropNameHindi = row.Field<string?>("CropNameHindi"),
                    FertilizerNameHindi = row.Field<string?>("FertilizerNameHindi"),
                    SocietyNameHi = row.Field<string?>("SocietyNameHi"),
                    NeedTime = row.Field<string?>("NeedTime"),
                    TotalCropAreaHec = row.Field<decimal>("TotalCropAreaHec"),
                    Status = row.Field<string?>("status"),
                    TotalFertilizerNeed = row.Field<decimal>("TotalFertilizerNeed"),
                    TotalAmount = row.Field<decimal>("TotalAmount"),
                    TotalCrops = row.Field<int>("TotalCrops"),
                    SuccessDemand = row.Field<decimal>("SuccessDemand"),
                    RecieveQty = row.Field<decimal>("RecieveQty"),
                    DemandDate = row.Field<DateTime>("DemandDate")
                });
            }
            return result;
        }

    }
}
