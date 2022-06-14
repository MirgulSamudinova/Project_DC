using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_DC.Migrations
{
    public partial class м5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Staffsid_staff",
                table: "ScheduleTemplate",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTemplate_Staffsid_staff",
                table: "ScheduleTemplate",
                column: "Staffsid_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTemplate_Staffs_Staffsid_staff",
                table: "ScheduleTemplate",
                column: "Staffsid_staff",
                principalTable: "Staffs",
                principalColumn: "id_staff");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTemplate_Staffs_Staffsid_staff",
                table: "ScheduleTemplate");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleTemplate_Staffsid_staff",
                table: "ScheduleTemplate");

            migrationBuilder.DropColumn(
                name: "Staffsid_staff",
                table: "ScheduleTemplate");
        }
    }
}
