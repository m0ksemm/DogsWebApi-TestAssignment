using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogsWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    DogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TailLength = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.DogID);
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "DogID", "Color", "Name", "TailLength", "Weight" },
                values: new object[,]
                {
                    { new Guid("58f8f0b2-0903-4f66-8d76-2bb8b769c4c1"), "red&amber", "Neo", 22, 32 },
                    { new Guid("e5c8c60c-1dca-40b3-9655-fa8457b5d6d8"), "black&white", "Jessy", 7, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_Name",
                table: "Dogs",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");
        }
    }
}
