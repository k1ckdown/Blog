using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Communities_CommunityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommunityId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CommunityId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                columns: new[] { "CommunityId", "UserId" });

            migrationBuilder.CreateTable(
                name: "CommunityAdmins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommunityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityAdmins", x => new { x.CommunityId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CommunityAdmins_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityAdmins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.InsertData(
                table: "CommunityAdmins",
                columns: new[] { "CommunityId", "UserId" },
                values: new object[,]
                {
                    { new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"), new Guid("52a94c73-6958-402c-8d1e-abe16e81cc22") },
                    { new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"), new Guid("51538053-0c9f-4139-a17c-996b09935c85") },
                    { new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"), new Guid("b2a55a66-33fd-471b-83ae-094dd6a3cda3") },
                    { new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"), new Guid("b2a55a66-33fd-471b-83ae-094dd6a3cda3") },
                    { new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"), new Guid("f200805b-dc0a-4340-8351-c92bf9a8d37c") },
                    { new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"), new Guid("cef7e70a-ce99-48d9-81a1-e18b1d34a7d6") },
                    { new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"), new Guid("67473056-077d-44e8-bbbf-f273072cce83") }
                });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cbbee647-d1db-4b9b-b053-f9f640bb97d8"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityAdmins_UserId",
                table: "CommunityAdmins",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityAdmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "CommunityId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                columns: new[] { "UserId", "CommunityId" });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 460, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 460, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 460, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 460, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 460, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cbbee647-d1db-4b9b-b053-f9f640bb97d8"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 3, 8, 25, 12, 458, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommunityId",
                table: "Users",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CommunityId",
                table: "Subscriptions",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Communities_CommunityId",
                table: "Users",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");
        }
    }
}
