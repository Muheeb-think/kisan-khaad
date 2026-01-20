using DAL.Repo;
using DAL.SqlHeplers;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface ICommonLogics
    {
        DataTable DropDownBind(dropdownBinderModel ddlbind);
    }

    public class CommonLogics : ICommonLogics
    {
        private readonly IDataAccess _dataAccess;

        public CommonLogics(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public DataTable DropDownBind(dropdownBinderModel ddlbind)
        {
            return _dataAccess.DropDownBind(ddlbind);
        }
    }

}
