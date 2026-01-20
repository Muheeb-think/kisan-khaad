using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    internal class CommonModels
    {
    }
    public class DDLCommonModals
    {
        public string? Action { get; set; }
    }

    public class dropdownBinderModel : DDLCommonModals
    {
        public string? Id { get; set; }
        public string? Value { get; set; }
        public int GetById { get; set; }

    }

}
