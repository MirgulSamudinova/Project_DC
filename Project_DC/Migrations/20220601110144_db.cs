using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project_DC.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

          

            migrationBuilder.CreateTable(
                name: "ServicesGroup",
                columns: table => new
                {
                    IdGroup = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesGroup", x => x.IdGroup);
                });

           

          
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    IdService = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    typeService = table.Column<string>(type: "text", nullable: false),
                    idGroup = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.IdService);
                    table.ForeignKey(
                        name: "FK_Services_ServicesGroup_idGroup",
                        column: x => x.idGroup,
                        principalTable: "ServicesGroup",
                        principalColumn: "IdGroup");
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Services_idGroup",
                table: "Services",
                column: "idGroup");

            

        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "Services");

          
            migrationBuilder.DropTable(
                name: "ServicesGroup");

          
        }
    }
}
