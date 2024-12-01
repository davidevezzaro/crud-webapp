using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB1.Migrations
{
    /// <inheritdoc />
    public partial class changeofcartable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicensePlateNumber",
                table: "Cars",
                newName: "PhoneNumberOwner");

            migrationBuilder.RenameColumn(
                name: "InsertionDate",
                table: "Cars",
                newName: "RegistrationDate");

            migrationBuilder.AddColumn<string>(
                name: "EmailNumberOwner",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Motor",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailNumberOwner",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Motor",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Cars",
                newName: "InsertionDate");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberOwner",
                table: "Cars",
                newName: "LicensePlateNumber");
        }
    }
}
