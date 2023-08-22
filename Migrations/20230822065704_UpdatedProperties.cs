using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashLeit_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionModelUserModel_Users_UsersId",
                table: "CollectionModelUserModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStats_Users_UserId",
                table: "UserStats");

            migrationBuilder.DropIndex(
                name: "IX_UserStats_UserId",
                table: "UserStats");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "CollectionModelUserModel",
                newName: "UsersUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionModelUserModel_UsersId",
                table: "CollectionModelUserModel",
                newName: "IX_CollectionModelUserModel_UsersUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cards",
                newName: "CardId");

            migrationBuilder.AddColumn<int>(
                name: "UserModelUserId",
                table: "UserStats",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Counters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_UserStats_UserModelUserId",
                table: "UserStats",
                column: "UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionModelUserModel_Users_UsersUserId",
                table: "CollectionModelUserModel",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStats_Users_UserModelUserId",
                table: "UserStats",
                column: "UserModelUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionModelUserModel_Users_UsersUserId",
                table: "CollectionModelUserModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStats_Users_UserModelUserId",
                table: "UserStats");

            migrationBuilder.DropIndex(
                name: "IX_UserStats_UserModelUserId",
                table: "UserStats");

            migrationBuilder.DropColumn(
                name: "UserModelUserId",
                table: "UserStats");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "CollectionModelUserModel",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionModelUserModel_UsersUserId",
                table: "CollectionModelUserModel",
                newName: "IX_CollectionModelUserModel_UsersId");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Cards",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Counters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_UserStats_UserId",
                table: "UserStats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionModelUserModel_Users_UsersId",
                table: "CollectionModelUserModel",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStats_Users_UserId",
                table: "UserStats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
