using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircleCoordinator.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "circleSet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_circleSet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coordinator",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    x = table.Column<int>(type: "integer", nullable: false),
                    y = table.Column<int>(type: "integer", nullable: false),
                    diameter = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    modifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CircleSetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinator", x => x.id);
                    table.ForeignKey(
                        name: "FK_coordinator_circleSet_CircleSetId",
                        column: x => x.CircleSetId,
                        principalTable: "circleSet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_coordinator_CircleSetId",
                table: "coordinator",
                column: "CircleSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coordinator");

            migrationBuilder.DropTable(
                name: "circleSet");
        }
    }
}
