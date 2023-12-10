using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.DTOs.Common;

public abstract class PaginationParameters
{
    [DefaultValue(DefaultPage)]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = DefaultPage;

    [DefaultValue(DefaultSize)]
    [Range(1, int.MaxValue)]
    public int Size { get; set; } = DefaultSize;

    private const int DefaultPage = 1;
    private const int DefaultSize = 5;
}