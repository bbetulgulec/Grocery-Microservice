using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace manav.cargo.dataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCompany",
                columns: table => new
                {
                    CargoCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCompany", x => x.CargoCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "CargoCustomer",
                columns: table => new
                {
                    CargoCustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerCity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCustomer", x => x.CargoCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "CargoOperation",
                columns: table => new
                {
                    CargoOperationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desciription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoOperation", x => x.CargoOperationID);
                });

            migrationBuilder.CreateTable(
                name: "CargoDetail",
                columns: table => new
                {
                    CargoDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<int>(type: "int", nullable: false),
                    CargoCompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoDetail", x => x.CargoDetailID);
                    table.ForeignKey(
                        name: "FK_CargoDetail_CargoCompany_CargoCompanyID",
                        column: x => x.CargoCompanyID,
                        principalTable: "CargoCompany",
                        principalColumn: "CargoCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoDetail_CargoCompanyID",
                table: "CargoDetail",
                column: "CargoCompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoCustomer");

            migrationBuilder.DropTable(
                name: "CargoDetail");

            migrationBuilder.DropTable(
                name: "CargoOperation");

            migrationBuilder.DropTable(
                name: "CargoCompany");
        }
    }
}
