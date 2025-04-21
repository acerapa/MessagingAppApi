using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBySetToNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // updating the old foreign key constraint to cascade delete
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Users_CreatedById",
                table: "Channels"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Users_CreatedById",
                table: "Channels",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Channels",
                type: "int",
                nullable: false,
                defaultValue: 1
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // updating the old foreign key constraint to set null
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Users_CreatedById",
                table: "Channels"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Users_CreatedById",
                table: "Channels",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Channels",
                type: "int",
                nullable: true,
                defaultValue: null
            );
        }
    }
}
