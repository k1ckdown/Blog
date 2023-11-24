using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity.Models;

public sealed class ApplicationUser : IdentityUser<Guid> {}