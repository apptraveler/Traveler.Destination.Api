using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Destinations.Api.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClimateStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinationAverageSpend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationAverageSpend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinationTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 800, nullable: false),
                    AverageSpendId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinations_DestinationAverageSpend_AverageSpendId",
                        column: x => x.AverageSpendId,
                        principalTable: "DestinationAverageSpend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookmarkedDestinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkedDestinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookmarkedDestinations_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DestinationClimateAverage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Min = table.Column<int>(type: "INTEGER", nullable: false),
                    Max = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationClimateAverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationClimateAverage_ClimateStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ClimateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DestinationClimateAverage_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DestinationImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationImages_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DestinationTagList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTagList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationTagList_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DestinationTagList_DestinationTags_TagId",
                        column: x => x.TagId,
                        principalTable: "DestinationTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteCoordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteCoordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteCoordinates_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClimateStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Quente" });

            migrationBuilder.InsertData(
                table: "ClimateStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Chuvoso" });

            migrationBuilder.InsertData(
                table: "ClimateStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Nevando" });

            migrationBuilder.InsertData(
                table: "ClimateStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Frio" });

            migrationBuilder.InsertData(
                table: "DestinationAverageSpend",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Baixo" });

            migrationBuilder.InsertData(
                table: "DestinationAverageSpend",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Médio" });

            migrationBuilder.InsertData(
                table: "DestinationAverageSpend",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Alto" });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Montanhas" });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Praias" });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Cachoeiras" });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Trilhas" });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Pontos Turísticos" });

            migrationBuilder.InsertData(
                table: "DestinationTags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Lugares Históricos" });

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkedDestinations_DestinationId",
                table: "BookmarkedDestinations",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationClimateAverage_DestinationId",
                table: "DestinationClimateAverage",
                column: "DestinationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DestinationClimateAverage_StatusId",
                table: "DestinationClimateAverage",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationImages_DestinationId",
                table: "DestinationImages",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_AverageSpendId",
                table: "Destinations",
                column: "AverageSpendId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTagList_DestinationId",
                table: "DestinationTagList",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTagList_TagId",
                table: "DestinationTagList",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteCoordinates_DestinationId",
                table: "RouteCoordinates",
                column: "DestinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookmarkedDestinations");

            migrationBuilder.DropTable(
                name: "DestinationClimateAverage");

            migrationBuilder.DropTable(
                name: "DestinationImages");

            migrationBuilder.DropTable(
                name: "DestinationTagList");

            migrationBuilder.DropTable(
                name: "RouteCoordinates");

            migrationBuilder.DropTable(
                name: "ClimateStatus");

            migrationBuilder.DropTable(
                name: "DestinationTags");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "DestinationAverageSpend");
        }
    }
}
