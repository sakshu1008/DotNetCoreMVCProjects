﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyNameColumnToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Vehicles");
        }
    }
}
