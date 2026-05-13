using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditMembershipConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Plans_PlanId",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Sessions_SessionId",
                table: "MemberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_SessionId",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "MemberShips");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "MemberShips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips",
                columns: new[] { "MemberId", "PlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Plans_PlanId",
                table: "MemberShips",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Plans_PlanId",
                table: "MemberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "MemberShips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "MemberShips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips",
                columns: new[] { "MemberId", "SessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_SessionId",
                table: "MemberShips",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Plans_PlanId",
                table: "MemberShips",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Sessions_SessionId",
                table: "MemberShips",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
