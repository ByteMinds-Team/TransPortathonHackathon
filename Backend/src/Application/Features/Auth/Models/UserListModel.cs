using Application.Common.Paging;
using Application.Features.Auth.Dtos;

namespace Application.Features.Auth.Models;

public class UserListModel: BasePageableModel
{
    public List<UserListDto> Items { get; set; }
}