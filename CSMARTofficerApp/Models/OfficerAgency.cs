using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CSMARTofficerApp.Models
{
    public partial class OfficerAgency
    {
        [Required(ErrorMessage = "Agency code cannot be null")]
        public string AgencyCode { get; set; }
        [RegularExpression("^[A-Z]+$", ErrorMessage ="Agency name must be in capital letters")]
        public string AgencyName { get; set; }
        public string AgencyState { get; set; }
        public int? Pincode { get; set; }
    }
}
