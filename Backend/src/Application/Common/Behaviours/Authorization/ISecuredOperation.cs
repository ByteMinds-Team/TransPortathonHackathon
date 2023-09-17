namespace Application.Common.Behaviours.Authorization;

public interface ISecuredRequest
{
    public string[] Roles { get; }
}