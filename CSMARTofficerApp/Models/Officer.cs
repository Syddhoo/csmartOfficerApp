using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CSMARTofficerApp.Models
{
    public partial class Officer
    {
        [Required]
        public string OfficerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PhoneNumber { get; set; }
        public int? BadgeNumber { get; set; }
        public string AgencyCode { get; set; }
    }
}
