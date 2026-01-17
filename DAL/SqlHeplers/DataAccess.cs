using DAL.ModelDTO;
using DAL.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SqlHeplers
{
    public interface IDataAccess
    {
        public DataTable UserAuthenticate(LoginDTO obj);

        public DataTable Registration(RegisterViewModel viewModel);
    }
    public class DataAccess : IDataAccess
    {
        private readonly IDBHelpers _dbHelper;

        public DataAccess(IDBHelpers dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DataTable Registration(RegisterViewModel obj)
        {

            SqlParameter[] parms = {
            // Personal Details
            new SqlParameter("@FullName", obj.FullName),
            new SqlParameter("@FatherHusbandName", obj.FatherHusbandName),
            new SqlParameter("@Gender", obj.Gender),
            new SqlParameter("@DOB", obj.DOB),
            new SqlParameter("@MobileNumber", obj.MobileNumber),
            new SqlParameter("@Email", (object)obj.Email ?? DBNull.Value),
            new SqlParameter("@PasswordHash", obj.Password),
            new SqlParameter("@FormerCategory", obj.FormerCategory),

            // Address Details
            new SqlParameter("@State", obj.State),
            new SqlParameter("@District", obj.District),
            new SqlParameter("@TahsilId", obj.tahsil),
            new SqlParameter("@VillageId", obj.village),
            new SqlParameter("@Address", obj.address),
            new SqlParameter("@AreaType", obj.AreaType),
            new SqlParameter("@Pincode", obj.pincode),

            // Land Details
            new SqlParameter("@LandRecordNumber", obj.LandRecordNumber),
            new SqlParameter("@TotalAreaAgristack", (object)obj.TotalAreaAgristack ?? DBNull.Value),
            new SqlParameter("@TotalArea", obj.TotalArea),
            new SqlParameter("@AreaofPaddySown", obj.AreaofPaddySown),
            new SqlParameter("@FarmerShare", obj.FarmerShare),

        };

            DataTable dt = _dbHelper.ExecuteDataTable("sp_InsertFarmerRegistration", parms);
            return dt;
        }

        public DataTable UserAuthenticate(LoginDTO obj)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Mobile", obj.Mobile),
            new SqlParameter("@Password", obj.Password),
        };
            DataTable ds = _dbHelper.ExecuteDataTable("UserAuthenticate", parm);
            return ds;
        }
    }
}
