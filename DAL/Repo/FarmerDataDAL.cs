using DAL.SqlHeplers;
using DAL.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repo
{
    public interface IFarmerDataDAL
    {
        public DataTable GetCorrespondenceFarmerData(string? MobileNo);
        public int UpdateCorrespondenceFarmerData(UserProfileViewModel obj);
        public DataSet FarmerDasBoardData(string? MobileNo);
        public DataTable GetFarmerFertilizerDemand(string? Action,string? UserMobile);
    }
    public class FarmerDataDAL : IFarmerDataDAL
    {
        private readonly IDBHelpers _dbHelper;

        public FarmerDataDAL(IDBHelpers dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataSet FarmerDasBoardData(string? MobileNo)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@MobileNo",MobileNo),
            };
            DataSet ds = _dbHelper.ExecuteDataSet("FarmerDashBoardData", parameters);
            return ds;
        }

        public DataTable GetCorrespondenceFarmerData(string? MobileNo)
        { 
            SqlParameter[] parameters = new SqlParameter[] {         
                new SqlParameter("@AadharNo", ""),
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@Action", "CorrespondenceDetials"),
            };
            DataTable dt = _dbHelper.ExecuteDataTable("selectorUpdateFarmerProfile", parameters);
            return dt;
        }

        public int UpdateCorrespondenceFarmerData(UserProfileViewModel obj)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@MobileNo", obj.Correspondence_MobileNo),
                new SqlParameter("@FarmerId", obj.FarmerId),
                new SqlParameter("@TehsilId", obj.Correspondence_TehsilId),
                new SqlParameter("@VillageId", obj.Correspondence_VillageId),
                new SqlParameter("@PinCode", obj.Correspondence_Pincode),
                new SqlParameter("@AadharNo", obj.AadharNo),
                new SqlParameter("@FullAddress", obj.Correspondence_FullAddresss),
                new SqlParameter("@Action", "UpdateCorrespondenceDetials"),
            };
            int result = _dbHelper.ExecuteInsert("selectorUpdateFarmerProfile", parameters);
            return result;
        }

        public DataTable GetFarmerFertilizerDemand(string? Action,string? userMobileNo)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                //new SqlParameter("@FarmerId", ""),
                new SqlParameter("@MobileNo", userMobileNo),
                new SqlParameter("@Action",Action),
            };
            DataTable dt = _dbHelper.ExecuteDataTable("Sp_GetFarmerFertilizerDemandStatus", parameters);
            return dt;
        }
    }
}
