using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project_DC.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FIO = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HealthData = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DentalServiceGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DentalServiceGroupName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalServiceGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToothSectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NumberOfSector = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToothSectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToothStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ToothStateName = table.Column<string>(type: "text", nullable: false),
                    ToothStateColor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToothStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToothTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ToothTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToothTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientsServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    StaffId = table.Column<int>(type: "integer", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientsServices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DentalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfService = table.Column<string>(type: "text", nullable: false),
                    DentalServiceGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsToothService = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DentalServices_DentalServiceGroups_DentalServiceGroupId",
                        column: x => x.DentalServiceGroupId,
                        principalTable: "DentalServiceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teeth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CurrentNumber = table.Column<int>(type: "integer", nullable: false),
                    ToothSectorId = table.Column<int>(type: "integer", nullable: false),
                    ToothTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teeth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teeth_ToothSectors_ToothSectorId",
                        column: x => x.ToothSectorId,
                        principalTable: "ToothSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teeth_ToothTypes_ToothTypeId",
                        column: x => x.ToothTypeId,
                        principalTable: "ToothTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientsTeeth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    ToothId = table.Column<int>(type: "integer", nullable: false),
                    ToothStateId = table.Column<int>(type: "integer", nullable: false),
                    ClientsServiceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsTeeth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientsTeeth_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsTeeth_ClientsServices_ClientsServiceId",
                        column: x => x.ClientsServiceId,
                        principalTable: "ClientsServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientsTeeth_Teeth_ToothId",
                        column: x => x.ToothId,
                        principalTable: "Teeth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsTeeth_ToothStates_ToothStateId",
                        column: x => x.ToothStateId,
                        principalTable: "ToothStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DentalServiceId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ClientsServiceId = table.Column<int>(type: "integer", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    ClientsToothId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralService_ClientsServices_ClientsServiceId",
                        column: x => x.ClientsServiceId,
                        principalTable: "ClientsServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeneralService_ClientsTeeth_ClientsToothId",
                        column: x => x.ClientsToothId,
                        principalTable: "ClientsTeeth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralService_DentalServices_DentalServiceId",
                        column: x => x.DentalServiceId,
                        principalTable: "DentalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientsServices_ClientId",
                table: "ClientsServices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsTeeth_ClientId",
                table: "ClientsTeeth",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsTeeth_ClientsServiceId",
                table: "ClientsTeeth",
                column: "ClientsServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsTeeth_ToothId",
                table: "ClientsTeeth",
                column: "ToothId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsTeeth_ToothStateId",
                table: "ClientsTeeth",
                column: "ToothStateId");

            migrationBuilder.CreateIndex(
                name: "IX_DentalServices_DentalServiceGroupId",
                table: "DentalServices",
                column: "DentalServiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralService_ClientsServiceId",
                table: "GeneralService",
                column: "ClientsServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralService_ClientsToothId",
                table: "GeneralService",
                column: "ClientsToothId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralService_DentalServiceId",
                table: "GeneralService",
                column: "DentalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Teeth_ToothSectorId",
                table: "Teeth",
                column: "ToothSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Teeth_ToothTypeId",
                table: "Teeth",
                column: "ToothTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralService");

            migrationBuilder.DropTable(
                name: "ClientsTeeth");

            migrationBuilder.DropTable(
                name: "DentalServices");

            migrationBuilder.DropTable(
                name: "ClientsServices");

            migrationBuilder.DropTable(
                name: "Teeth");

            migrationBuilder.DropTable(
                name: "ToothStates");

            migrationBuilder.DropTable(
                name: "DentalServiceGroups");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ToothSectors");

            migrationBuilder.DropTable(
                name: "ToothTypes");

           
        }
    }
}
