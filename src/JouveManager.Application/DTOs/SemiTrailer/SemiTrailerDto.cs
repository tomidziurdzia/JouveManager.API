using JouveManager.Domain.Enum;

namespace JouveManager.Application.DTOs.SemiTrailer;

public class SemiTrailerDto
{
    public Guid Id { get; set; }
    public string? LicensePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? ImageUrl { get; set; }
    public TypeSemiTrailer? Type { get; set; }
}
