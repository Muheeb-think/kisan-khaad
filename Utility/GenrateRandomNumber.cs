using System;

namespace Utilities
{
    public static  class GenrateRandomNumber  
    {
        public static string GenerateOTP()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            string s = number.Remove(6);

            return s;
        }
    }
}
