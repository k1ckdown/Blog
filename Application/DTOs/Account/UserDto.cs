using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DTOs.Account;

public sealed class UserDto : IMapFrom<User>
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public string phoneNumber { get; set; }
}