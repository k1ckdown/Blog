using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder
            .HasMany(community => community.Administrators);

        builder
            .HasMany(community => community.Subscribers)
            .WithMany(subscriber => subscriber.Subscriptions)
            .UsingEntity(joinEntity => joinEntity.ToTable("Subscribers"));

        builder
            .HasData(
                CreateCommunity(
                    "84367e0d-5b35-4ae1-81ef-ce2ba6974f19", 
                    "IT Geeks",
                    "Сообщество разработчиков, где можно делиться опытом, обсуждать новейшие технологии и находить интересные проекты.",
                    true),
                
                CreateCommunity(
                    "92d6b5bb-4977-4507-a281-9872a2f93590",
                    "Sport Enthusiasts",
                    "Сообщество спортивных энтузиастов, где можно обсуждать тренировки, соревнования и делиться своими достижениями в спорте.",
                    false),
                
                CreateCommunity(
                    "01a705ae-7f35-46a5-b8d6-e07be527893b", 
                    "English Lovers", 
                    "Сообщество любителей английского языка, где можно улучшить свои навыки, общаясь с носителями языка и принимая участие в языковых встречах.",
                    false),
                
                CreateCommunity(
                    "a7aba6b2-31ce-45d4-be78-17ff89a3b04a", 
                    "Auto Enthusiasts", "Сообщество автолюбителей, где можно обсуждать последние новости в автомобильной индустрии, делииться опытом по тюнингу авто и проводить встречи на автошоу.",
                    true),
                
                CreateCommunity(
                    "53817554-2518-406d-b1f6-4b1f2e4cedc3", 
                    "Tech Startups",
                    "Сообщество стартапов в сфере информационных технологий, где можно найти соучредителей, получить обратную связь на идеи и найти инвесторов для своего проекта.",
                    true)
                );
    }

    private static Community CreateCommunity(string id, string name, string description, bool isClosed) =>
        new Community
        {
            Id = new Guid(id),
            Name = name,
            Description = description,
            IsClosed = isClosed,
            CreateTime = DateTime.UtcNow
        };
}