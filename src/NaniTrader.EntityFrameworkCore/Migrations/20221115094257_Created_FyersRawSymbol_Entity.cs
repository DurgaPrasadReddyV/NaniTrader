using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaniTrader.Migrations
{
    public partial class Created_FyersRawSymbol_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppFyersRawSymbols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column16 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column17 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Column18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFyersRawSymbols", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFyersRawSymbols");
        }
    }
}
