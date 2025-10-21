using Microsoft.AspNetCore.Mvc;
using PingIdentityApp.Services.Certification;
using Microsoft.AspNetCore.Authorization;
using PingIdentityApp.Services.PingOne;
using GetChatty.Data;

namespace PingIdentityApp.Controllers;

[Authorize]
public class CertificationsControlController : Controller
{
    private readonly ICertificationService _certificationService;
    private readonly IPingOneManagementService _pingOneManagementService;
    private readonly GetChattyDataContext _dataContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CertificationsControlController"/> class.
    /// </summary>
    /// <param name="certificationService"></param>
    public CertificationsControlController(
        ICertificationService certificationService,
        IPingOneManagementService pingOneManagementService,
        GetChattyDataContext dataContext)
    {
        ArgumentNullException.ThrowIfNull(certificationService);
        ArgumentNullException.ThrowIfNull(pingOneManagementService);
        ArgumentNullException.ThrowIfNull(dataContext);

        _certificationService = certificationService;
        _pingOneManagementService = pingOneManagementService;
        _dataContext = dataContext;
    }

    /// <summary>
    /// Index action to display access requests.
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        var requests = _dataContext.AccessRequests.ToList();
        return View("Index", requests);
    }

    /// <summary>
    /// History action to display access requests history.
    /// </summary>
    /// <returns></returns>
    public IActionResult History()
    {
        var requests = _dataContext.AccessRequests.Where(r => r.Status != "Pending").ToList();
        return View("History", requests);
    }

    /// <summary>
    /// Manage action to display user and group management.
    /// </summary>
    /// <returns></returns>
    public IActionResult Manage()
    {
        return View("Manage");
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

    /// <summary>
    /// ProvisionGroupMembership action to provision a user's group membership.
    /// </summary>
    /// <param name="usedId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProvisionGroupMembership(string userId, string groupId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(groupId);

        TempData["Message"] = "Group membership provisioned.";

        await _pingOneManagementService.ProvisionGroupMembershipAsync(userId, groupId);
        return RedirectToAction("Manage", "CertificationsControl");
    }

    /// <summary>
    /// ProvisionGroupMembership action to provision a user's group membership.
    /// </summary>
    /// <param name="usedId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeprovisionGroupMembership(string userId, string groupId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(userId);
        ArgumentNullException.ThrowIfNullOrEmpty(groupId);

        TempData["Message"] = "Group membership deprovisioned.";

        await _pingOneManagementService.DeprovisionGroupMembershipAsync(userId, groupId);
        return RedirectToAction("Manage", "CertificationsControl");
    }
}