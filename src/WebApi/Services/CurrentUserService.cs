using ImposterSyndrome.Application.Common.Interfaces;
using System.Security.Claims;

namespace ImposterSyndrome.WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}