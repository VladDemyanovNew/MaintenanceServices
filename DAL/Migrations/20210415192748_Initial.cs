using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VDemyanov.MaintenanceServices.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONTRACT_CATEGORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTRACT_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EQUIPMENT_CATEGORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPMENT_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "POSITION",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSITION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRICE_LIST",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CREATION_DATE = table.Column<DateTime>(type: "date", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRICE_LIST", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONTRACT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "date", nullable: true),
                    CLIENT_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FACILITY_ADDRESS = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CATEGORY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTRACT", x => x.ID);
                    table.ForeignKey(
                        name: "CONTRACT_CATEGORY_FK",
                        column: x => x.CATEGORY,
                        principalTable: "CONTRACT_CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EQUIPMENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CATEGORY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPMENT", x => x.ID);
                    table.ForeignKey(
                        name: "EQUIPMENT_CATEGORY_FK",
                        column: x => x.CATEGORY,
                        principalTable: "EQUIPMENT_CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOGIN = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    POSITION = table.Column<int>(type: "int", nullable: true),
                    BDAY = table.Column<DateTime>(type: "date", nullable: true),
                    AVATAR_PATH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                    table.ForeignKey(
                        name: "POSITION_FK",
                        column: x => x.POSITION,
                        principalTable: "POSITION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SERVICE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRICE_LIST = table.Column<int>(type: "int", nullable: true),
                    UNIT = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    PRICE = table.Column<decimal>(type: "money", nullable: true),
                    NOTATION = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICE", x => x.ID);
                    table.ForeignKey(
                        name: "PRICE_LIST_FK",
                        column: x => x.PRICE_LIST,
                        principalTable: "PRICE_LIST",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REPORT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CONTRACT = table.Column<int>(type: "int", nullable: true),
                    DISCOUNT = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPORT", x => x.ID);
                    table.ForeignKey(
                        name: "CONTRACT_FK",
                        column: x => x.CONTRACT,
                        principalTable: "CONTRACT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SERVICE_EQUIPMENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SERVICE_ID = table.Column<int>(type: "int", nullable: true),
                    EQUIPMENT_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICE_EQUIPMENT", x => x.ID);
                    table.ForeignKey(
                        name: "EQUIPMENT_ID_FK",
                        column: x => x.EQUIPMENT_ID,
                        principalTable: "EQUIPMENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SERVICE_ID_FK",
                        column: x => x.SERVICE_ID,
                        principalTable: "SERVICE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REPORT_DATA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    REPORT = table.Column<int>(type: "int", nullable: true),
                    SERVICE_EQUIPMENT = table.Column<int>(type: "int", nullable: true),
                    NUMBER = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPORT_DATA", x => x.ID);
                    table.ForeignKey(
                        name: "REPORT_FK",
                        column: x => x.REPORT,
                        principalTable: "REPORT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SERVICE_EQUIPMENT_FK",
                        column: x => x.SERVICE_EQUIPMENT,
                        principalTable: "SERVICE_EQUIPMENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTRACT_CATEGORY",
                table: "CONTRACT",
                column: "CATEGORY");

            migrationBuilder.CreateIndex(
                name: "UQ__CONTRACT__D9C1FA00F1D7EC60",
                table: "CONTRACT",
                column: "NAME",
                unique: true,
                filter: "[NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__CONTRACT__4193D92E30ED3023",
                table: "CONTRACT_CATEGORY",
                column: "DESCRIPTION",
                unique: true,
                filter: "[DESCRIPTION] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__CONTRACT__D9C1FA00534C47D2",
                table: "CONTRACT_CATEGORY",
                column: "NAME",
                unique: true,
                filter: "[NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_POSITION",
                table: "EMPLOYEE",
                column: "POSITION");

            migrationBuilder.CreateIndex(
                name: "UQ__EMPLOYEE__E39E2665A0764FBA",
                table: "EMPLOYEE",
                column: "LOGIN",
                unique: true,
                filter: "[LOGIN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPMENT_CATEGORY",
                table: "EQUIPMENT",
                column: "CATEGORY");

            migrationBuilder.CreateIndex(
                name: "UQ__EQUIPMEN__4193D92EE3C083BC",
                table: "EQUIPMENT_CATEGORY",
                column: "DESCRIPTION",
                unique: true,
                filter: "[DESCRIPTION] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__EQUIPMEN__D9C1FA00A794AA4D",
                table: "EQUIPMENT_CATEGORY",
                column: "NAME",
                unique: true,
                filter: "[NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__POSITION__D9C1FA00E7775FB3",
                table: "POSITION",
                column: "NAME",
                unique: true,
                filter: "[NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_REPORT_CONTRACT",
                table: "REPORT",
                column: "CONTRACT");

            migrationBuilder.CreateIndex(
                name: "IX_REPORT_DATA_REPORT",
                table: "REPORT_DATA",
                column: "REPORT");

            migrationBuilder.CreateIndex(
                name: "IX_REPORT_DATA_SERVICE_EQUIPMENT",
                table: "REPORT_DATA",
                column: "SERVICE_EQUIPMENT");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_PRICE_LIST",
                table: "SERVICE",
                column: "PRICE_LIST");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_EQUIPMENT_EQUIPMENT_ID",
                table: "SERVICE_EQUIPMENT",
                column: "EQUIPMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_EQUIPMENT_SERVICE_ID",
                table: "SERVICE_EQUIPMENT",
                column: "SERVICE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "REPORT_DATA");

            migrationBuilder.DropTable(
                name: "POSITION");

            migrationBuilder.DropTable(
                name: "REPORT");

            migrationBuilder.DropTable(
                name: "SERVICE_EQUIPMENT");

            migrationBuilder.DropTable(
                name: "CONTRACT");

            migrationBuilder.DropTable(
                name: "EQUIPMENT");

            migrationBuilder.DropTable(
                name: "SERVICE");

            migrationBuilder.DropTable(
                name: "CONTRACT_CATEGORY");

            migrationBuilder.DropTable(
                name: "EQUIPMENT_CATEGORY");

            migrationBuilder.DropTable(
                name: "PRICE_LIST");
        }
    }
}
