using Blog.Application.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreateTime { get; set; }
}