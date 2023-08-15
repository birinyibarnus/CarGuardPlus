using CarGuardPlus.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarGuardPlus.BLL
{
    public class SendAlertService : ISendAlertService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public SendAlertService
            (ApplicationDbContext context,
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public async Task SendAlert(string licence, string message)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var recieverUser = _context.Users.Where(x => x.Id == GetLicence(licence).UserId).FirstOrDefault();
                var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                var alert = new AlertMessage
                {
                    Message = message,
                    Timestamp = DateTime.Now,
                    ReceiverUser = recieverUser,
                    ReceiverUserId = recieverUser.Id,
                    SenderUser = currentUser,
                    SenderUserId = currentUser.Id
                };

                _context.AlertMessages.Add(alert);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public Licence? GetLicence(string licence)
        {
            return _context.Licences.Where(x => x.LicencePlate == licence).FirstOrDefault();
        }
    }
    public interface ISendAlertService
    {
        Task SendAlert(string licence, string message);
        Licence? GetLicence(string licence);
    };
}
