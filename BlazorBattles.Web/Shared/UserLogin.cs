using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorBattles.Web.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Please enter username")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
