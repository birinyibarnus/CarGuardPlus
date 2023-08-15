using CarGuardPlus.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarGuardPlus.BLL
{
    public class MyAlertService : IMyAlertService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public MyAlertService(
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _context = context;
        }
        public async IAsyncEnumerable<AlertMessage> GetAlerts()
        {
            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var alerts = await _context.AlertMessages
                .Where(x => x.ReceiverUserId == Convert.ToString(currentUser.Id))
                .ToListAsync();
            var sortedAlerts = alerts.OrderByDescending(x => x.Timestamp);
            foreach (var alert in sortedAlerts)
            {
                yield return alert;
            }
        }

    }
    public interface IMyAlertService
    {
        IAsyncEnumerable<AlertMessage> GetAlerts();
    }
}
