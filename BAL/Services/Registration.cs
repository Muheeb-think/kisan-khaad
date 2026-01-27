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
    public interface IRegistration
    {
        public int FarmerRegistration(RegisterViewModel registerView);
        public DataTable FarmerDetailsbyId(FarmerSearchRequest request);
        public DataTable FertilizerDetailsById(FertilizerDetails obj);
        public ApiResponse<List<TempDemandVM>> FertilizerDemand(TempraryDemand obj);

    }
    public class Registration: IRegistration
    {
        public readonly IDataAccess _dataaccess;
        public Registration(IDataAccess dataAccess)
        {
            _dataaccess = dataAccess;    
        }
        public DataTable FarmerDetailsbyId(FarmerSearchRequest request)
        {
            DataTable dt = _dataaccess.FarmerDetailsById(request);
            return dt;
        }

        public DataTable FertilizerDetailsById(FertilizerDetails obj)
        {
            DataTable dt = _dataaccess.FertilizerDetailsById(obj);
            return dt;
        }
        public int FarmerRegistration(RegisterViewModel registerView)
        {
            DataTable landDt = ToDataTable(registerView.LandList);
            int result = _dataaccess.FarmerRegistration(registerView, landDt);
            return result;
        }


        public static DataTable ToDataTable(List<LandViewModel> landList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("DistrictId", typeof(int));
            dt.Columns.Add("TehsilId", typeof(int));
            dt.Columns.Add("VillageId", typeof(int));
            dt.Columns.Add("KhasraNo", typeof(string));
            dt.Columns.Add("TotalArea", typeof(decimal));
            dt.Columns.Add("FarmerShareArea", typeof(decimal));

            if (landList == null || landList.Count == 0)
                return dt;

            foreach (var land in landList)
            {
                dt.Rows.Add(
                    1,
                    land.TahsilId,
                    land.VillageId,
                    land.LandRecordNumber ?? string.Empty,
                    land.LandTotalArea,
                    land.FarmerShare
                );
            }

            return dt;
        }
        public ApiResponse<List<TempDemandVM>> FertilizerDemand(TempraryDemand obj)
        {
            DataSet ds = _dataaccess.FertilizerDemand(obj);

            List<TempDemandVM> list = new();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new TempDemandVM
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FarmerId = Convert.ToInt32(row["FarmerId"]),
                    CropNameHindi = row["CropNameHindi"].ToString(),
                    FertilizerNameHindi = row["FertilizerNameHindi"].ToString(),
                    CropAreaHec = Convert.ToDecimal(row["CropAreaHec"]),
                    FertilizerNeed = Convert.ToDecimal(row["FertilizerNeed"]),
                    Rate_Kg = Convert.ToDecimal(row["Rate_Kg"]),
                    Amount = Convert.ToDecimal(row["Amount"])
                });
            }

            string msg = ds.Tables[1].Rows[0]["Message"].ToString();

            return new ApiResponse<List<TempDemandVM>>
            {
                Status = true,
                Message = msg,
                Data = list
            };
        }
    }
}
