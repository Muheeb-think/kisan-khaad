using DAL.ModelDTO;
using DAL.SqlHeplers;
using DAL.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface IDataAccess
    {
        public DataTable UserAuthenticate(LoginDTO obj);
        public DataTable DropDownBind(dropdownBinderModel ddlbind);
        //public DataTable Registration(RegisterViewModel viewModel);
        public DataTable FarmerDetailsById(FarmerSearchRequest request);
        public DataTable FertilizerDetailsById(FertilizerDetails obj);
        public int FarmerRegistration(RegisterViewModel viewModel, DataTable dt);

    }
    public class DataAccess : IDataAccess
    {
        private readonly IDBHelpers _dbHelper;

        public DataAccess(IDBHelpers dbHelper)
        {
            _dbHelper = dbHelper;
        }

        //public DataTable Registration(RegisterViewModel obj)
        //{

        //    SqlParameter[] parms = {
        //    // Personal Details
        //    new SqlParameter("@FullName", obj.FullName),
        //    new SqlParameter("@FatherHusbandName", obj.FatherHusbandName),
        //    new SqlParameter("@Gender", obj.Gender),
        //    new SqlParameter("@DOB", obj.DOB),
        //    new SqlParameter("@MobileNumber", obj.MobileNumber),
        //    new SqlParameter("@Email", (object)obj.Email ?? DBNull.Value),
        //    new SqlParameter("@PasswordHash", obj.Password),
        //    new SqlParameter("@FormerCategory", obj.FormerCategory),

        //    // Address Details
        //    new SqlParameter("@State", obj.State),
        //    new SqlParameter("@District", obj.District),
        //    new SqlParameter("@TahsilId", obj.tahsil),
        //    new SqlParameter("@VillageId", obj.village),
        //    new SqlParameter("@Address", obj.address),
        //    new SqlParameter("@AreaType", obj.AreaType),
        //    new SqlParameter("@Pincode", obj.pincode),

        //    // Land Details
        //    new SqlParameter("@LandRecordNumber", obj.LandRecordNumber),
        //    new SqlParameter("@TotalAreaAgristack", (object)obj.TotalAreaAgristack ?? DBNull.Value),
        //    new SqlParameter("@TotalArea", obj.TotalArea),
        //    new SqlParameter("@AreaofPaddySown", obj.AreaofPaddySown),
        //    new SqlParameter("@FarmerShare", obj.FarmerShare),

        //};

        //    DataTable dt = _dbHelper.ExecuteDataTable("sp_InsertFarmerRegistration", parms);
        //    return dt;
        //}

        public DataTable UserAuthenticate(LoginDTO obj)
        {
            SqlParameter[] parm = {
            new SqlParameter("@Mobile", obj.Mobile),
            new SqlParameter("@Password", obj.Password),
        };
            DataTable ds = _dbHelper.ExecuteDataTable("UserAuthenticate", parm);
            return ds;
        }
        #region Mukeem
        public DataTable DropDownBind(dropdownBinderModel ddlbind)
        {
            SqlParameter[] parms =
            {
           new SqlParameter("@Action", ddlbind.Action ?? ""),
           new SqlParameter("@GetById", ddlbind.GetById)
       };

            return _dbHelper.ExecuteDataTable("Sp_BindDDL", parms);
        }
        public DataTable FarmerDetailsById(FarmerSearchRequest request)
        {
            SqlParameter[] parms =
            {
           new SqlParameter("@Action", request.Action ?? ""),
           new SqlParameter("@farmerId", request.FarmerId)
       };

            return _dbHelper.ExecuteDataTable("SP_FarmerDetailsById", parms);
        }

        public DataTable FertilizerDetailsById(FertilizerDetails obj)
        {
            SqlParameter[] parms =
            {
           new SqlParameter("@Action", obj.Action ?? ""),
           new SqlParameter("@CropId", obj.CropId)
       };

            return _dbHelper.ExecuteDataTable("SP_FertilizerDetailsById", parms);
        }
        public int FarmerRegistration(RegisterViewModel obj, DataTable landDetails)
        {
            try
            {
                SqlParameter resultOutParam = new SqlParameter("@result_out", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter[] parms =
                {
             // Farmer Details
             new SqlParameter("@FarmerName", obj.FarmerName ?? (object)DBNull.Value),
             new SqlParameter("@FarmerCategoryId", (object)obj.FormerCategory ?? DBNull.Value),
             new SqlParameter("@Gender", obj.Gender ?? (object)DBNull.Value),
             new SqlParameter("@MobileNo", obj.MobileNumberAgriStack ?? (object)DBNull.Value),
             new SqlParameter("@Email", (object)obj.Email ?? DBNull.Value),
             new SqlParameter("@DOB", obj.DOB.HasValue ? obj.DOB.Value : (object)DBNull.Value),
             new SqlParameter("@AadharCardNo", obj.AadharCardNo ?? (object)DBNull.Value),
             new SqlParameter("@FatherOrHusbandName", obj.FatherHusbandName ?? (object)DBNull.Value),
             new SqlParameter("@DistrictId", obj.District == "जालौन" ? "1" : "0"),
             new SqlParameter("@TehsilId", (object)obj.tahsilAgriStack ?? DBNull.Value),
             new SqlParameter("@BlockId", DBNull.Value),
             new SqlParameter("@VillageId", (object)obj.village ?? DBNull.Value),
             new SqlParameter("@Pincode", obj.pincodeAgriStack ?? (object)DBNull.Value),

             // Correspondence Address
             new SqlParameter("@CorrespondenceMobileNo", obj.MobileNumber ?? (object)DBNull.Value),
             new SqlParameter("@CorrespondenceDistrictId", obj.District == "जालौन" ? "1" : "0"),
             new SqlParameter("@CorrespondenceTehsilId", (object)obj.tahsilId ?? DBNull.Value),
             new SqlParameter("@CorrespondenceBlockId", DBNull.Value),
             new SqlParameter("@CorrespondenceVillageId", (object)obj.village ?? DBNull.Value),
             new SqlParameter("@CorrespondencePincode", obj.pincode ?? (object)DBNull.Value),
             new SqlParameter("@CorrespondenceFullAddress", obj.address ?? (object)DBNull.Value),

             // Other
             new SqlParameter("@Password", obj.Password ?? (object)DBNull.Value),
             new SqlParameter("@KhataNo", DBNull.Value),
             new SqlParameter("@Declaration", true),
             new SqlParameter("@AgriStackId", obj.FarmerId ?? (object)DBNull.Value),

             // TABLE VALUED PARAMETER
             new SqlParameter
             {
                 ParameterName = "@FarmerLandDetail",
                 SqlDbType = SqlDbType.Structured,
                 TypeName = "FarmerLandDetails",
                 Value = landDetails ?? (object)DBNull.Value
             },

             resultOutParam
          };

                int result = _dbHelper.ExecuteInsert("SP_FarmerRegistration", parms);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

    }
}
