using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThuchanhAPIvoinetcore.Migrations
{
    public partial class AddPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
            */
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Students",
                type: "varchar(max)",
                nullable: true); // Thay đổi này giả định rằng cột Photo có thể null
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "Students");
            */
            // Lệnh để loại bỏ cột nếu cần rollback migration
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Students");
        }
    }
}
