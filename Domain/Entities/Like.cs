using Domain.Common;

namespace Domain.Entities;

public sealed class Like : BaseEntity
{
    public required User User { get; set; }
}