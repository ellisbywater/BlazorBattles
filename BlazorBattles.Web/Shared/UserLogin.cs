using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorBattles.Web.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Please enter your email address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
