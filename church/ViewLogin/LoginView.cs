using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace church.ViewLogin
{
    public class LoginView
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}