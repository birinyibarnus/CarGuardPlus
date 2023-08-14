using CarGuardPlus.BLL;
using Microsoft.AspNetCore.Identity;

namespace CarGuardPlus.Areas.Identity.Data
{
    public class Licence
    {
        public int LicenceId { get; set; }
        public string LicencePlate { get; set; }
        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }
        public List<AlertMessage> ReceivedAlertMessages { get; set; }
    }
}
