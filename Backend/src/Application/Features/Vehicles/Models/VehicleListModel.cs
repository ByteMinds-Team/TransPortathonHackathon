using Application.Common.Paging;

namespace Application.Features.Vehicles.Models;

public class VehicleListModel : BasePageableModel
{
    public List<VehicleListDto> Items { get; set; }
}