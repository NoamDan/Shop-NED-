using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Shope.Migrations
{
    public partial class noam4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberPhone",
                table: "Customer",
                newName: "NumberHome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberHome",
                table: "Customer",
                newName: "NumberPhone");
        }
    }
}
