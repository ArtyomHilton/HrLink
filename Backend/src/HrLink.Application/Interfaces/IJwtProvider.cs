using HrLink.Application.DTOs;

namespace HrLink.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(LoginDataResponse data);
}