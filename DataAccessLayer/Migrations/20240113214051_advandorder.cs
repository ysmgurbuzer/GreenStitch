using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class advandorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvertIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvertId = table.Column<int>(type: "int", nullable: false),
                    AdvertTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePathOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePathTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePathThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "OrderHistoryAdvertisements",
                columns: table => new
                {
                    AdvertisementsId = table.Column<int>(type: "int", nullable: false),
                    OrderHistoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryAdvertisements", x => new { x.AdvertisementsId, x.OrderHistoriesId });
                    table.ForeignKey(
                        name: "FK_OrderHistoryAdvertisements_Advertisements_AdvertisementsId",
                        column: x => x.AdvertisementsId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHistoryAdvertisements_OrderHistories_OrderHistoriesId",
                        column: x => x.OrderHistoriesId,
                        principalTable: "OrderHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_MemberId",
                table: "Advertisements",
                column: "MemberId");



            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_MemberId",
                table: "OrderHistories",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryAdvertisements_OrderHistoriesId",
                table: "OrderHistoryAdvertisements",
                column: "OrderHistoriesId");
        }

           

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "OrderHistoryAdvertisements");

           
            migrationBuilder.DropTable(
                name: "OrderHistories");

           

            migrationBuilder.DropTable(
                name: "Advertisements");

           
        }
    }
}
