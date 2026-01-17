using DAL.SqlHeplers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace BAL.MasterData
{
    public interface IMastersData
    {
        public DataSet SelectTehsil();
        public DataTable SelectBlock(int Tehsil);
        public DataTable SelectVillageMaster(int blockId);
    }
    public class MasterData : IMastersData
    {
        private readonly IDBHelpers _dataAccess;
        public MasterData(IDBHelpers dataAccess)
        {
            this._dataAccess = dataAccess;
        }
        public DataTable SelectBlock(int Tehsil)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@Tehsil",Tehsil)
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SelectBlock", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }

        public DataSet SelectTehsil()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                { };
                DataSet ds = _dataAccess.ExecuteDataSet("SelectTehsilMaster", parameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }

        public DataTable SelectVillageMaster(int blockId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@BlockId",blockId)
                };
                DataTable dt = _dataAccess.ExecuteDataTable("SelectVillageMaster", parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Execption from bal:" + ex.Message);
            }
        }
    }
}
