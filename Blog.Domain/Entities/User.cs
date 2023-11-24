using Blog.Domain.Common.Interfaces;

namespace Blog.Domain.Entities;

public sealed class User : IEntity
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public enum Gender
{
    Male,
    Female
}