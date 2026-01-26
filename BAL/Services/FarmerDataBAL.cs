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
    public class IFarmerDataBAL
    {
      //  public List<UserProfileViewModel> SelectCorrespondenceFarmerData();
    }
    public class FarmerDataBal : IFarmerDataBAL
    {
        private readonly IFarmerDataDAL _farmerdal;
        public FarmerDataBal(IFarmerDataDAL farmerDataDAL)
        {
            this._farmerdal= farmerDataDAL;    
        }
        //public List<UserProfileViewModel> SelectCorrespondenceFarmerData()
        //{
        //    DataTable dt = _farmerdal.GetCorrespondenceFarmerData();
        //}
    }
}
