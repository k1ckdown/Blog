using Blog.Domain.Common;

namespace Blog.Domain.Entities;

public sealed class User : BaseEntity
{
    public required DateTime CreateTime { get; set; }
    public required string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public required Gender Gender { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }

    public IList<Post> Posts { get; set; }
    public IList<Post> FavoritePosts { get; set; }
    public IList<Community> Subscriptions { get; set; }
    public IList<Community> AdministeredCommunities { get; set; }
}

public enum Gender
{
    Male,
    Female
}