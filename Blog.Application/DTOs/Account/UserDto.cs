using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Account;

public sealed class UserDto : IMapFrom<User>
{
    public required Guid Id { get; set; }
    public required DateTime CreateTime { get; set; }
    public required string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public required Gender Gender { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
}