using BAL.Services;
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
        public DataTable GetCorrespondenceFarmerData(UserProfileViewModel obj);
    }
    public class FarmerDataDAL : IFarmerDataDAL
    {
        private readonly IDBHelpers _dbHelper;

        public FarmerDataDAL(IDBHelpers dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable GetCorrespondenceFarmerData(UserProfileViewModel obj)
        {
            DataTable dt = new();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@MobileNo", obj.Correspondence_MobileNo),
                new SqlParameter("@FarmerId", obj.FarmerId),
                new SqlParameter("@TehsilId", obj.TehsilList),
                new SqlParameter("@VillageId", obj.Correspondence_VillageId),
                new SqlParameter("@PinCode", obj.Correspondence_Pincode),
                new SqlParameter("@AadharNo", obj.AadharNo),
                new SqlParameter("@FullAddress", obj.Correspondence_FullAddresss),
                new SqlParameter("@Action", obj.Action),
            };

            return dt;
        }
    }
}
