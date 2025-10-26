using HrLink.API.DTOs.Auth;
using HrLink.Application.DTOs;

namespace HrLink.API.Mappings;

public static class RoleMapping
{
    public static RoleResponse ToResponse(this RoleDataResponse data) =>
        new RoleResponse(data.Id, data.RoleName);

    public static List<RoleResponse> ToResponse(this ICollection<RoleDataResponse> data) =>
        data.Select(x => x.ToResponse())
            .ToList();
}