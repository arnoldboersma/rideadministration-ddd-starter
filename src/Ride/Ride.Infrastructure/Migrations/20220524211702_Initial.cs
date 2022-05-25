using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideAdministration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ride");

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "ride",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "rideseq",
                schema: "ride",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ridestopseq",
                schema: "ride",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ride",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RideStopCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ride",
                schema: "ride",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ride", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ridestop",
                schema: "ride",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RideId = table.Column<int>(type: "int", nullable: false),
                    Delivered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ridestop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ridestop_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ride",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ridestop_ride_RideId",
                        column: x => x.RideId,
                        principalSchema: "ride",
                        principalTable: "ride",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ridestop_OrderId",
                schema: "ride",
                table: "ridestop",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ridestop_RideId",
                schema: "ride",
                table: "ridestop",
                column: "RideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ridestop",
                schema: "ride");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ride");

            migrationBuilder.DropTable(
                name: "ride",
                schema: "ride");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "ride");

            migrationBuilder.DropSequence(
                name: "rideseq",
                schema: "ride");

            migrationBuilder.DropSequence(
                name: "ridestopseq",
                schema: "ride");
        }
    }
}
