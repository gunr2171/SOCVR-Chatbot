using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace SOCVR.Chatbot.Migrations
{
    public partial class AcceptedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PermissionRequest_User_RequestingUserId", table: "PermissionRequest");
            migrationBuilder.DropForeignKey(name: "FK_PermissionRequest_User_ReviewingUserId", table: "PermissionRequest");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_User_UserId", table: "UserPermission");
            migrationBuilder.DropForeignKey(name: "FK_UserReviewedItem_User_ReviewerId", table: "UserReviewedItem");
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "PermissionRequest",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRequest_User_RequestingUserId",
                table: "PermissionRequest",
                column: "RequestingUserId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRequest_User_ReviewingUserId",
                table: "PermissionRequest",
                column: "ReviewingUserId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_User_UserId",
                table: "UserPermission",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserReviewedItem_User_ReviewerId",
                table: "UserReviewedItem",
                column: "ReviewerId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PermissionRequest_User_RequestingUserId", table: "PermissionRequest");
            migrationBuilder.DropForeignKey(name: "FK_PermissionRequest_User_ReviewingUserId", table: "PermissionRequest");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_User_UserId", table: "UserPermission");
            migrationBuilder.DropForeignKey(name: "FK_UserReviewedItem_User_ReviewerId", table: "UserReviewedItem");
            migrationBuilder.DropColumn(name: "Accepted", table: "PermissionRequest");
            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRequest_User_RequestingUserId",
                table: "PermissionRequest",
                column: "RequestingUserId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRequest_User_ReviewingUserId",
                table: "PermissionRequest",
                column: "ReviewingUserId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_User_UserId",
                table: "UserPermission",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserReviewedItem_User_ReviewerId",
                table: "UserReviewedItem",
                column: "ReviewerId",
                principalTable: "User",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
