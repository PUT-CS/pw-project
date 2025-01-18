using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barski_Lewandowski_DAOEF.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    ProducerId = table.Column<int>(type: "INTEGER", nullable: false),
                    HandleMaterial = table.Column<int>(type: "INTEGER", nullable: false),
                    FibersMaterial = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brooms_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brooms_ProducerId",
                table: "Brooms",
                column: "ProducerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brooms");

            migrationBuilder.DropTable(
                name: "Producers");
        }
    }
}
