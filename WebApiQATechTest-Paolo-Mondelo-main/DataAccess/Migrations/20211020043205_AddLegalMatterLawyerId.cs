using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddLegalMatterLawyerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LawyerId",
                table: "Matter",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matter_LawyerId",
                table: "Matter",
                column: "LawyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matter_Lawyer_LawyerId",
                table: "Matter",
                column: "LawyerId",
                principalTable: "Lawyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matter_Lawyer_LawyerId",
                table: "Matter");

            migrationBuilder.DropIndex(
                name: "IX_Matter_LawyerId",
                table: "Matter");

            migrationBuilder.DropColumn(
                name: "LawyerId",
                table: "Matter");
        }
    }
}
