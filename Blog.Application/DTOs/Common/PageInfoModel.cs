namespace Blog.Application.DTOs.Common;

public sealed class PageInfoModel
{
    public required int Size { get; set; }
    public required int Count { get; set; }
    public required int Current { get; set; }
}