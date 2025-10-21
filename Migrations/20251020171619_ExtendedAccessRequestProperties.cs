using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedAccessRequestProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AccessRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "AccessRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AccessRequests",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AccessRequests");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "AccessRequests");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AccessRequests");
        }
    }
}
