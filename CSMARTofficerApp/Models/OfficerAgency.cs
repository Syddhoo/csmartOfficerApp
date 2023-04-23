using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CSMARTofficerApp.Models
{
    public partial class OfficerAgency
    {
        [Required]
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string AgencyState { get; set; }
        public int? Pincode { get; set; }
    }
}
