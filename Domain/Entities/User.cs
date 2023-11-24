using Domain.Common;

namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public required DateTime CreateTime { get; set; }
    public required string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public required Gender Gender { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public enum Gender
{
    Male,
    Female
}