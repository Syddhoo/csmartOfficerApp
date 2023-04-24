using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CSMARTofficerApp.Models
{
    public partial class Officer
    {
        [Required(ErrorMessage ="Officer key cannot be null")]
        public string OfficerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[Required(ErrorMessage ="Phone number is required")]
        //[Display (Name = "PhoneNumber")]
        [RegularExpression(@"^[1-9]\d{8}$", ErrorMessage = "Phone number should be 9 digit long and must not start with 0")]
        public int? PhoneNumber { get; set; }
        public int? BadgeNumber { get; set; }
        public string AgencyCode { get; set; }
    }
}
