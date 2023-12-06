using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                newName: "IX_Comments_ParentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                columns: new[] { "UserId", "CommunityId" });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 585, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 585, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 585, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 585, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 585, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cbbee647-d1db-4b9b-b053-f9f640bb97d8"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                column: "CreateTime",
                value: new DateTime(2023, 12, 6, 14, 4, 31, 583, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CommunityId",
                table: "Subscriptions",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CommunityId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                newName: "IX_Comments_CommentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Comments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                columns: new[] { "CommunityId", "UserId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
