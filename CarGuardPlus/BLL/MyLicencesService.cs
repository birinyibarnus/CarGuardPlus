using CarGuardPlus.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CarGuardPlus.BLL
{
    public class MyLicencesService : MyAlertService, IMyLicencesService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public MyLicencesService(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext context) : base(contextAccessor, userManager, context)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _context = context;
        }
        public async IAsyncEnumerable<Licence> GetLicences()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            if (currentUser is not null)
            {
                var licences = await _context.Licences
                    .Where(x => x.UserId == Convert.ToString(currentUser.Id))
                    .ToListAsync();
                foreach (var licence in licences)
                {
                    yield return licence;
                }
            }
        }
        public async Task<bool> AddLicencePlate(string licencePlate)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var recieverUser = _context.Users.Where(x => x.Id == currentUser.Id).FirstOrDefault();
                var licence = new Licence
                {
                    LicencePlate = licencePlate,
                    User = currentUser,
                    UserId = currentUser.Id,
                    ReceivedAlertMessages = currentUser.ReceivedAlertMessages
                };
                if (!LicenceAlreadyExist(licence.LicencePlate))
                {
                    _context.Licences.Add(licence);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
            return false;
        }
        public bool LicenceAlreadyExist(string licencePlate)
        {
            return  _context.Licences.Any(x => x.LicencePlate == licencePlate);
        }
        public async Task DeleteLicencePlate(string licencePlate)
        {
            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var licence = await _context.Licences.Where(x => x.LicencePlate == licencePlate && x.UserId == currentUser.Id).FirstOrDefaultAsync();
            if (licence is not null)
                _context.Licences.Remove(licence);
            await _context.SaveChangesAsync();
        }
    }
    public interface IMyLicencesService
    {
        IAsyncEnumerable<Licence> GetLicences();
        Task<bool> AddLicencePlate(string licencePlate);
        Task DeleteLicencePlate(string licencePlate);
        bool LicenceAlreadyExist(string licencePlate);
    }
}
