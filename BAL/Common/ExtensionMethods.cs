using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Common
{
    internal class ExtensionMethods
    {


    }
    public static class DataTableExtensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<T> list = new List<T>();

            // Get the properties of the target type T
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (DataRow row in table.AsEnumerable())
            {
                T obj = new T();
                foreach (PropertyInfo prop in properties)
                {
                    // Check if the DataTable contains a column with the same name as the property
                    if (table.Columns.Contains(prop.Name))
                    {
                        object value = row[prop.Name];

                        // Handle DBNull.Value and nullable types
                        if (value != DBNull.Value)
                        {
                            Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                object safeValue = Convert.ChangeType(value, t);
                                prop.SetValue(obj, safeValue, null);
                            }
                            catch (InvalidCastException)
                            {
                                // Handle potential type mismatch or conversion errors gracefully
                                // You might log this error or apply a default value
                            }
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }

    public static class Constant
    {
        public const string किसान = "किसान";
        public const string संस्था = "संस्था";
        public const string ज़िला = "ज़िला";

    }
    public static class SessionHelper
    {
        private static  string usermobile;
        public static string UserMobile
        {

            get { return usermobile; }
            set { usermobile = value; }
        }
        private static string userid;
        public static string UserId
        {

            get { return userid; }
            set { userid = value; }
        }
        private static string userrole;
        public static string UserRole
        {

            get { return userrole; }
            set { userrole = value; }
        }
        private static string username;
        public static string UserName
        {

            get { return username; }
            set { username = value; }
        }
        private static bool isauthgenticated;
        public static bool IsAuthenticated
        {

            get { return isauthgenticated; }
            set { isauthgenticated = value; }
        }


    }
}
