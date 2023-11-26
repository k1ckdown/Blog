using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "as_addr_obj",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    typename = table.Column<string>(type: "text", nullable: false),
                    isactual = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_addr_obj", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_adm_hierarchy",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<int>(type: "integer", nullable: false),
                    parentobjid = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_adm_hierarchy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_houses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<int>(type: "integer", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    housenum = table.Column<string>(type: "text", nullable: true),
                    housetype = table.Column<int>(type: "integer", nullable: true),
                    isactual = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false),
                    addnum1 = table.Column<string>(type: "text", nullable: true),
                    addnum2 = table.Column<string>(type: "text", nullable: true),
                    addtype1 = table.Column<int>(type: "integer", nullable: true),
                    addtype2 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_houses", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_obj");

            migrationBuilder.DropTable(
                name: "as_adm_hierarchy");

            migrationBuilder.DropTable(
                name: "as_houses");
        }
    }
}
