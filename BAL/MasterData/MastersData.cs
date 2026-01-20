//using DAL.SqlHeplers;
//using DAL.ViewModel;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;


//namespace BAL.MasterData
//{
//    public interface IMastersData
//    {
//        public DataSet SelectTehsil();
//        public DataTable SelectBlock(int Tehsil);
//        public DataTable SelectVillageMaster(int? blockId);
//        public int InsertVillage(VillageViewMaster obj);
//        public DataTable SelectCropMaster();
//        public int InsertCropMaster();
//    }
//    public class MasterData : IMastersData
//    {
//        private readonly IDBHelpers _dataAccess;
//        public MasterData(IDBHelpers dataAccess)
//        {
//            this._dataAccess = dataAccess;
//        }

//        public int InsertVillage(VillageViewMaster obj)
//        {
//            SqlParameter[] parameters = new SqlParameter[]
//            {
//                new SqlParameter("@VillageNameHindi",obj.VillageNameHindi),
//                new SqlParameter("@UserId","1")
//            };
//            int result = _dataAccess.ExecuteInsert("InsertVillage", parameters);
//            return result;
//        }

//        public DataTable SelectBlock(int Tehsil)
//        {
//            try
//            {
//                SqlParameter[] parameters = new SqlParameter[]
//                {
//                new SqlParameter("@Tehsil",Tehsil)
//                };
//                DataTable dt = _dataAccess.ExecuteDataTable("SelectBlock", parameters);
//                return dt;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Execption from bal:" + ex.Message);
//            }
//        }

//        public DataSet SelectTehsil()
//        {
//            try
//            {
//                SqlParameter[] parameters = new SqlParameter[]
//                { };
//                DataSet ds = _dataAccess.ExecuteDataSet("SelectTehsilMaster", parameters);
//                return ds;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Execption from bal:" + ex.Message);
//            }
//        }

//        public DataTable SelectVillageMaster(int? blockId)
//        {
//            try
//            {
//                SqlParameter[] parameters = new SqlParameter[]
//                {
//                new SqlParameter("@BlockId",blockId ?? 0)
//                };
//                DataTable dt = _dataAccess.ExecuteDataTable("SelectVillageMaster", parameters);
//                return dt;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Execption from bal:" + ex.Message);
//            }
//        }

//        public DataTable SelectCropMaster()
//        {
//            try
//            {
//                SqlParameter[] parameters = new SqlParameter[]
//                {           
//                };
//                DataTable dt = _dataAccess.ExecuteDataTable("SelectCrop", parameters);
//                return dt;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Execption from bal:" + ex.Message);
//            }
//        }

//        public int InsertCropMaster()
//        {
//            SqlParameter[] parameters = new SqlParameter[]
//            {
//                //new SqlParameter("@VillageNameHindi",obj.VillageNameHindi),
//                new SqlParameter("@UserId","1")
//            };
//            int result = _dataAccess.ExecuteInsert("InsertVillage", parameters);
//            return result;
//        }
//    }
//}
