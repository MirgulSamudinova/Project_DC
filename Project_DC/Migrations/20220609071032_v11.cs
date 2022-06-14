using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project_DC.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTemplate_Staffs_Staffsid_staff",
                table: "ScheduleTemplate");

            migrationBuilder.RenameColumn(
                name: "Staffsid_staff",
                table: "ScheduleTemplate",
                newName: "DaysOfWeekID");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleTemplate_Staffsid_staff",
                table: "ScheduleTemplate",
                newName: "IX_ScheduleTemplate_DaysOfWeekID");

            migrationBuilder.CreateTable(
                name: "DaysOfWeek",
                columns: table => new
                {
                    DaysOfWeekID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DaysOfWeekName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeek", x => x.DaysOfWeekID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTemplate_StaffId",
                table: "ScheduleTemplate",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTemplate_DaysOfWeek_DaysOfWeekID",
                table: "ScheduleTemplate",
                column: "DaysOfWeekID",
                principalTable: "DaysOfWeek",
                principalColumn: "DaysOfWeekID");

        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTemplate_DaysOfWeek_DaysOfWeekID",
                table: "ScheduleTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTemplate_Staffs_StaffId",
                table: "ScheduleTemplate");

            migrationBuilder.DropTable(
                name: "DaysOfWeek");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleTemplate_StaffId",
                table: "ScheduleTemplate");

            migrationBuilder.RenameColumn(
                name: "DaysOfWeekID",
                table: "ScheduleTemplate",
                newName: "Staffsid_staff");

          
        }
    }
}
