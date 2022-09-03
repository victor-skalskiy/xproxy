using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XProxy_DAL.Migrations
{
    public partial class TelegramFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramAdminChatId",
                table: "UserSettings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TelegramBotToken",
                table: "UserSettings",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramAdminChatId",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "TelegramBotToken",
                table: "UserSettings");
        }
    }
}
