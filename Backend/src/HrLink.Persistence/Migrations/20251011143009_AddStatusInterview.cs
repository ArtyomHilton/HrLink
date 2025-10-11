using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrLink.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusInterview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "StatusId",
                table: "Interview",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false),
                    StatusName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_StatusId",
                table: "Interview",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_StatusName",
                table: "Status",
                column: "StatusName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_Status_StatusId",
                table: "Interview",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_Status_StatusId",
                table: "Interview");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Interview_StatusId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Interview");
        }
    }
}
