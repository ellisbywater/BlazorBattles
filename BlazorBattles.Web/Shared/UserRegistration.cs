using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorBattles.Web.Shared
{
    public class UserRegistration
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(16, ErrorMessage="Too long")]
        public string Username { get; set; }
        public string Bio { get; set; }
        [Required, StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage ="Passwords must match")]
        public string ConfirmPassword { get; set; }
        [Range(300, 1000, ErrorMessage = "Choose a number between 300 & 1000")]
        public int Bananas { get; set; } = 300;
        public string StartUnitId { get; set; } = "1";
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please Confirm")]
        public bool IsConfirmed { get; set; } = true;
    }
}
