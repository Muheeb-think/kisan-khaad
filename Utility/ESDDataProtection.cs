using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.ESDDataProtection
{
   public  interface IESDDataProtection  
    {
        string EncryptData(string PlainData);
        string DecryptData(string EncryptedData);
    }
    public class ESDDataProtection : IESDDataProtection
    {
        private IDataProtector dataProtector;
        private IConfiguration configuration;
        public ESDDataProtection(IDataProtectionProvider _dataProtector, IConfiguration _configuration)
        {
            configuration = _configuration;
            dataProtector = _dataProtector.CreateProtector("Admin@123!!!");
        }
        public string EncryptData(string PlainData)
        {
            return dataProtector.Protect(PlainData);
        }
        public string DecryptData(string EncryptedData)
        {
            return dataProtector.Unprotect(EncryptedData);
        }
    }
    public class ESDDataProtectionModelAttribute : IESDDataProtection
    {
        private IDataProtector dataProtector;
        private readonly IConfiguration configuration;
        public ESDDataProtectionModelAttribute(IDataProtectionProvider _dataProtector, IConfiguration _configuration)
        {
            configuration = _configuration;
            dataProtector = _dataProtector.CreateProtector("Admin@123!!!");
        }
        public string EncryptData(string PlainData)
        {
            return dataProtector.Protect(PlainData);
        }
        public string DecryptData(string EncryptedData)
        {
            return dataProtector.Unprotect(EncryptedData);
        }
    }
}
