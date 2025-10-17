using Microsoft.AspNetCore.Mvc;
using PingIdentityApp.Services.Certification;
using Microsoft.AspNetCore.Authorization;

namespace PingIdentityApp.Controllers
{
    [Authorize]
    public class CertificationsControlController : Controller
    {
        private readonly ICertificationService _certificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificationsControlController"/> class.
        /// </summary>
        /// <param name="certificationService"></param>
        public CertificationsControlController(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        /// <summary>
        /// Index action to display access requests.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var requests = _certificationService.AccessRequests;
            return View("Index", requests);
        }

        /// <summary>
        /// History action to display access requests history.
        /// </summary>
        /// <returns></returns>
        public IActionResult History()
        {
            var requests = _certificationService.AccessRequestsHistory;
            return View("History", requests);
        }

        /// <summary>
        /// ApproveRequest action to approve an access request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(string requestId)
        {
            await _certificationService.ApproveAccessRequestAsync(requestId);
            TempData["Message"] = "Access request approved!";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// DenyRequest action to deny an access request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DenyRequest(string requestId)
        {
            await _certificationService.DenyAccessRequestAsync(requestId);
            TempData["Message"] = "Access request denied.";
            return RedirectToAction("Index");
        }
    }
}
