using Domain.Common;

namespace Domain.Entities;

public sealed class Post : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int ReadingTime { get; set; }
    public string? Image { get; set; } 
    public required DateTime CreateTime { get; set; }
    public Guid? AddressId { get; set; }
    public List<Tag> Tags { get; set; } = new();
    public List<Like> Likes { get; set; } = new();
    
    public User User { get; set; }
    public required Guid UserId { get; set; }
}

// id*	string($uuid)
// createTime*	string($date-time)
// title*	string
// minLength: 1
// description*	string
// minLength: 1
// readingTime*	integer($int32)
// image	string
// nullable: true
// authorId*	string($uuid)
// author*	string
// minLength: 1
// communityId	string($uuid)
// nullable: true
// communityName	string
// nullable: true
// addressId	string($uuid)
// nullable: true
// likes*	integer($int32)
// default: 0
// hasLike*	boolean
// default: false
// commentsCount*	integer($int32)
// default: 0
// tags	[...]
// comments*	[...]