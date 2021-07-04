using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hahn.ApplicationProcess.February2021.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: false),
                    CountryOfDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddressOfDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Broken = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Assets", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}