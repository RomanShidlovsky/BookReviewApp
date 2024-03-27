namespace Identity.BusinessLogic.DTOs.RequestDTOs;

public sealed record RefreshTokenDto(string AccessToken, string RefreshToken);