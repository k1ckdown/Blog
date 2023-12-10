using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreateTime", "Description", "IsClosed", "Name" },
                values: new object[,]
                {
                    { new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"), new DateTime(2023, 12, 2, 17, 45, 39, 84, DateTimeKind.Utc).AddTicks(9140), "Сообщество любителей английского языка, где можно улучшить свои навыки, общаясь с носителями языка и принимая участие в языковых встречах.", false, "English Lovers" },
                    { new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"), new DateTime(2023, 12, 2, 17, 45, 39, 84, DateTimeKind.Utc).AddTicks(9140), "Сообщество стартапов в сфере информационных технологий, где можно найти соучредителей, получить обратную связь на идеи и найти инвесторов для своего проекта.", true, "Tech Startups" },
                    { new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"), new DateTime(2023, 12, 2, 17, 45, 39, 84, DateTimeKind.Utc).AddTicks(9130), "Сообщество разработчиков, где можно делиться опытом, обсуждать новейшие технологии и находить интересные проекты.", true, "IT Geeks" },
                    { new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"), new DateTime(2023, 12, 2, 17, 45, 39, 84, DateTimeKind.Utc).AddTicks(9140), "Сообщество спортивных энтузиастов, где можно обсуждать тренировки, соревнования и делиться своими достижениями в спорте.", false, "Sport Enthusiasts" },
                    { new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"), new DateTime(2023, 12, 2, 17, 45, 39, 84, DateTimeKind.Utc).AddTicks(9140), "Сообщество автолюбителей, где можно обсуждать последние новости в автомобильной индустрии, делииться опытом по тюнингу авто и проводить встречи на автошоу.", true, "Auto Enthusiasts" }
                });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3450));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3450));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3450));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3450));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3450));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cbbee647-d1db-4b9b-b053-f9f640bb97d8"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 2, 17, 45, 39, 83, DateTimeKind.Utc).AddTicks(3450));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cbbee647-d1db-4b9b-b053-f9f640bb97d8"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                column: "CreateTime",
                value: new DateTime(2023, 11, 30, 16, 55, 53, 667, DateTimeKind.Utc).AddTicks(5700));
        }
    }
}
