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
        public DataTable FarmerRegistration(RegisterViewModel registerView);
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
        public DataTable FarmerRegistration(RegisterViewModel registerView)
        {
            throw new NotImplementedException();
        }

    }
}
