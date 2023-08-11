using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarGuardPlus.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Licence> Licences { get; set; }
    public List<AlertMessage> SentAlertMessages { get; set; }
    public List<AlertMessage> ReceivedAlertMessages { get; set; }
}

