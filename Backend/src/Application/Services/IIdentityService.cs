namespace Application.Services;

public interface IIdentityService
{
    bool IsInRole(string[] roles);
}