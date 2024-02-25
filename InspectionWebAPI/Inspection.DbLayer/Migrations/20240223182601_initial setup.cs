using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inspection.DbLayer.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mstInspector",
                columns: table => new
                {
                    InspectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectorName = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "True")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mstInspector", x => x.InspectorId);
                });

            migrationBuilder.CreateTable(
                name: "TxnInspection",
                columns: table => new
                {
                    InspectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TxnInspection", x => x.InspectionID);
                    table.ForeignKey(
                        name: "FK_TxnInspection_mstInspector_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "mstInspector",
                        principalColumn: "InspectorId");
                });

            migrationBuilder.InsertData(
                table: "mstInspector",
                columns: new[] { "InspectorId", "InspectorName" },
                values: new object[] { 1, "Inspector 1" });

            migrationBuilder.InsertData(
                table: "mstInspector",
                columns: new[] { "InspectorId", "InspectorName" },
                values: new object[] { 2, "Inspector 2" });

            migrationBuilder.InsertData(
                table: "mstInspector",
                columns: new[] { "InspectorId", "InspectorName" },
                values: new object[] { 3, "Inspector 3" });

            migrationBuilder.CreateIndex(
                name: "IX_TxnInspection_InspectorId",
                table: "TxnInspection",
                column: "InspectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TxnInspection");

            migrationBuilder.DropTable(
                name: "mstInspector");
        }
    }
}
