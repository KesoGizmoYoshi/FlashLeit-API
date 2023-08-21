using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashLeit_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IncorrectAnswerOne = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IncorrectAnswerTwo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IncorrectAnswerThree = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CollectionModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Collections_CollectionModelId",
                        column: x => x.CollectionModelId,
                        principalTable: "Collections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionModelUserModel",
                columns: table => new
                {
                    CollectionsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionModelUserModel", x => new { x.CollectionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CollectionModelUserModel_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionModelUserModel_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsAchieved = table.Column<bool>(type: "bit", nullable: false),
                    UserStatsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_UserStats_UserStatsModelId",
                        column: x => x.UserStatsModelId,
                        principalTable: "UserStats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    AmountOfCardsAnswered = table.Column<int>(type: "int", nullable: false),
                    AmountOfCorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    AmountOfIncorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    AmountOfPerfectRuns = table.Column<int>(type: "int", nullable: false),
                    TimesStarted = table.Column<int>(type: "int", nullable: false),
                    TimesFinished = table.Column<int>(type: "int", nullable: false),
                    UserStatsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counters_UserStats_UserStatsModelId",
                        column: x => x.UserStatsModelId,
                        principalTable: "UserStats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_UserStatsModelId",
                table: "Achievements",
                column: "UserStatsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CollectionModelId",
                table: "Cards",
                column: "CollectionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionModelUserModel_UsersId",
                table: "CollectionModelUserModel",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_UserStatsModelId",
                table: "Counters",
                column: "UserStatsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStats_UserId",
                table: "UserStats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "CollectionModelUserModel");

            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.DropTable(
                name: "TestTable");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "UserStats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
