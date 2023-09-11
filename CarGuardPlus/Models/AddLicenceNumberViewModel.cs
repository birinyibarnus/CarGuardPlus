using CarGuardPlus.Areas.Identity.Data;
using CarGuardPlus.BLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarGuardPlus.Models
{
    public class AddLicenceNumberViewModel
    {
        public bool LicenceAlreadyExist { get; set; }
        public IAsyncEnumerable<Licence> ListOfLicences { get; set; }
    }
}
