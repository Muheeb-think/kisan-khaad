using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Mobile number requried !!")]
        public string? Mobile { get; set; }
        [Required(ErrorMessage ="Password is required !!")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Captcha Code Required")]
        [StringLength(5)]
        public string CaptchaCode { get; set; }
        public string CaptchaImg { get; set; }
        //public string SeedKey { get; set; }
    }
}
