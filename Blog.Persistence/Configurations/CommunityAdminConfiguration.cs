using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Persistence.Configurations;

public sealed class CommunityAdminConfiguration : IEntityTypeConfiguration<CommunityAdmin>
{
    public void Configure(EntityTypeBuilder<CommunityAdmin> builder)
    {
        builder
            .HasData(
                CreateCommunityAdmin("b2a55a66-33fd-471b-83ae-094dd6a3cda3", "53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                CreateCommunityAdmin("51538053-0c9f-4139-a17c-996b09935c85", "53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                CreateCommunityAdmin("f200805b-dc0a-4340-8351-c92bf9a8d37c", "84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                CreateCommunityAdmin("b2a55a66-33fd-471b-83ae-094dd6a3cda3", "84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                CreateCommunityAdmin("cef7e70a-ce99-48d9-81a1-e18b1d34a7d6", "92d6b5bb-4977-4507-a281-9872a2f93590"),
                CreateCommunityAdmin("67473056-077d-44e8-bbbf-f273072cce83", "a7aba6b2-31ce-45d4-be78-17ff89a3b04a"),
                CreateCommunityAdmin("52a94c73-6958-402c-8d1e-abe16e81cc22", "01a705ae-7f35-46a5-b8d6-e07be527893b"));
    }

    private static CommunityAdmin CreateCommunityAdmin(string userId, string communityId) =>
        new() { UserId = new Guid(userId), CommunityId = new Guid(communityId) };
}