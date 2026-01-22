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
    }
}
