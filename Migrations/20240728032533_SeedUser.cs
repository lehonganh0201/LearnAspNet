using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnPageRazor.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<string>(
            //     name: "Name",
            //     table: "UserTokens",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "UserTokens",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ProviderKey",
            //     table: "UserLogins",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "UserLogins",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(128)",
            //     oldMaxLength: 128);

            for (int i = 0; i < 150; i++)
            {
                migrationBuilder.InsertData(
                    "Users",
                    columns: new[]
                    {
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "HomeAddress"
                    },
                    values: new object[]
                    {
                        Guid.NewGuid().ToString(),
                        "User-" + i.ToString("D3"),
                        $"email{i.ToString("D3")}@example.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        false,
                        0,
                        "...@#%..."
                    }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<string>(
            //     name: "Name",
            //     table: "UserTokens",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "UserTokens",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ProviderKey",
            //     table: "UserLogins",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "LoginProvider",
            //     table: "UserLogins",
            //     type: "nvarchar(128)",
            //     maxLength: 128,
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");
        }
    }
}
