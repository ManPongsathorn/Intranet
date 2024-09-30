using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    public partial class FirstIntranetDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryComplaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryComplaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintFroms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintFroms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HubConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameTH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameTH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BgUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    InternalPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentId = table.Column<int>(type: "int", nullable: false),
                    DirectoryGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_CategoryComplaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryComplaintId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_CategoryComplaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "log_ComplaintFroms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintFromId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_ComplaintFroms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    CategoryComplaintId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Complaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Department1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department1Id = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Department1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Department2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department2Id = table.Column<int>(type: "int", nullable: false),
                    Department1Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Department2s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_DirectoryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectoryGroupId = table.Column<int>(type: "int", nullable: false),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_DirectoryGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikeId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CheckLike = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Likes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_LocationGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_LocationGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LocationGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Medias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_MenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_MenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    ShopPositionGroupId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Department1Id = table.Column<int>(type: "int", nullable: false),
                    Department2Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "log_ShopGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_ShopGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_ShopPositionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopPositionGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_ShopPositionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    ShopGroupId = table.Column<int>(type: "int", nullable: false),
                    ShopPositionGroupId = table.Column<int>(type: "int", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_UserConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectionOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_UserConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_UserMenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMenuGroupId = table.Column<int>(type: "int", nullable: false),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionByLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_UserMenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopPositionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPositionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryComplaintId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaints_CategoryComplaints_CategoryComplaintId",
                        column: x => x.CategoryComplaintId,
                        principalTable: "CategoryComplaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_LocationGroups_LocationGroupId",
                        column: x => x.LocationGroupId,
                        principalTable: "LocationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectoryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectoryGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectoryGroups_MenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_MenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department1s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department1s_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectoryGroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_DirectoryGroups_DirectoryGroupId",
                        column: x => x.DirectoryGroupId,
                        principalTable: "DirectoryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CheckLike = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department1Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department2s_Department1s_Department1Id",
                        column: x => x.Department1Id,
                        principalTable: "Department1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department1Id = table.Column<int>(type: "int", nullable: false),
                    Department2Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Department1s_Department1Id",
                        column: x => x.Department1Id,
                        principalTable: "Department1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Department2s_Department2Id",
                        column: x => x.Department2Id,
                        principalTable: "Department2s",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    ShopPositionGroupId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_ShopPositionGroups_ShopPositionGroupId",
                        column: x => x.ShopPositionGroupId,
                        principalTable: "ShopPositionGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    ShopGroupId = table.Column<int>(type: "int", nullable: false),
                    ShopPositionGroupId = table.Column<int>(type: "int", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shops_ShopGroups_ShopGroupId",
                        column: x => x.ShopGroupId,
                        principalTable: "ShopGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shops_ShopPositionGroups_ShopPositionGroupId",
                        column: x => x.ShopPositionGroupId,
                        principalTable: "ShopPositionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameTH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameTH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BgUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    InternalPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountPassword = table.Column<int>(type: "int", nullable: false),
                    ResetPassword = table.Column<bool>(type: "bit", nullable: false),
                    PasswordLock = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuGroups_MenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3082f730-c6e3-48ea-aace-7ba7fed8b100", null, "User", "USER" },
                    { "74ad35f0-58ec-4f58-b929-613762177f4a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "BgUser", "ConcurrencyStamp", "CreateBy", "CreateOn", "Email", "EmailConfirmed", "EmployeeId", "FirstNameEN", "FirstNameTH", "ImageUser", "InternalPhoneNumber", "LastNameEN", "LastNameTH", "LockoutEnabled", "LockoutEnd", "NickName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "SecurityStamp", "ShopId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "23c6f6a5-0677-43fc-995d-93bb32a57e36", 0, true, null, "4f5eedd1-26e9-44f9-ae1f-b495f9321e1c", "Admin Admin", new DateTime(2023, 12, 28, 11, 31, 14, 917, DateTimeKind.Local).AddTicks(4219), "System@moshimoshi.co.th", false, null, "Admin", "แอดมิน", null, null, "Admin", "แอดมิน", true, null, null, "SYSTEM@MOSHIMOSHI.CO.TH", "SYSTEM@MOSHIMOSHI.CO.TH", "AQAAAAEAACcQAAAAENWvpbMkc+o7HG13B7ZCaKsTaoojBZuwQWun/iCHBD/73R4yjRWRfulIRl5ciJDlTw==", null, false, null, "43e96247-52c4-4a5b-a553-4c009b5c1a2d", null, false, "System@moshimoshi.co.th" },
                    { "a39ed126-7e8b-4478-8f05-58bfec8f59b7", 0, true, null, "f224e42f-87aa-4582-9af0-85c8e2b37d6d", "Admin Admin", new DateTime(2023, 12, 28, 11, 31, 14, 917, DateTimeKind.Local).AddTicks(4235), "comsec@moshimoshi.co.th", false, null, "Moshi", "Moshi", null, null, "Moshi", "Moshi", true, null, null, "COMSEC@MOSHIMOSHI.CO.TH", "COMSEC@MOSHIMOSHI.CO.TH", "AQAAAAEAACcQAAAAEB6HSFbTTdH4gRHmhNv3DeDgs4GFM/hI7i0lZzaDiRoVzGTZU6O9sE6bFb0lJmIxsQ==", null, false, null, "9720c656-c67c-48ed-905c-db6ebd05868f", null, false, "comsec@moshimoshi.co.th" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "74ad35f0-58ec-4f58-b929-613762177f4a", "23c6f6a5-0677-43fc-995d-93bb32a57e36" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3082f730-c6e3-48ea-aace-7ba7fed8b100", "a39ed126-7e8b-4478-8f05-58bfec8f59b7" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShopId",
                table: "AspNetUsers",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_DirectoryGroupId",
                table: "Attachments",
                column: "DirectoryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_CategoryComplaintId",
                table: "Complaints",
                column: "CategoryComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_Department1s_LocationId",
                table: "Department1s",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Department2s_Department1Id",
                table: "Department2s",
                column: "Department1Id");

            migrationBuilder.CreateIndex(
                name: "IX_DirectoryGroups_MenuGroupId",
                table: "DirectoryGroups",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationGroupId",
                table: "Locations",
                column: "LocationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_PostId",
                table: "Medias",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PostId",
                table: "Notifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistories_UserId",
                table: "PasswordHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SectionId",
                table: "Positions",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ShopPositionGroupId",
                table: "Positions",
                column: "ShopPositionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MenuGroupId",
                table: "Posts",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Department1Id",
                table: "Sections",
                column: "Department1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Department2Id",
                table: "Sections",
                column: "Department2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_SectionId",
                table: "Shops",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopGroupId",
                table: "Shops",
                column: "ShopGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopPositionGroupId",
                table: "Shops",
                column: "ShopPositionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuGroups_MenuGroupId",
                table: "UserMenuGroups",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuGroups_UserId",
                table: "UserMenuGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ComplaintFroms");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "HubConnections");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Log_ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Log_Attachments");

            migrationBuilder.DropTable(
                name: "Log_CategoryComplaints");

            migrationBuilder.DropTable(
                name: "Log_Comments");

            migrationBuilder.DropTable(
                name: "log_ComplaintFroms");

            migrationBuilder.DropTable(
                name: "Log_Complaints");

            migrationBuilder.DropTable(
                name: "Log_Department1s");

            migrationBuilder.DropTable(
                name: "Log_Department2s");

            migrationBuilder.DropTable(
                name: "Log_DirectoryGroups");

            migrationBuilder.DropTable(
                name: "Log_Likes");

            migrationBuilder.DropTable(
                name: "Log_LocationGroups");

            migrationBuilder.DropTable(
                name: "Log_Locations");

            migrationBuilder.DropTable(
                name: "Log_Medias");

            migrationBuilder.DropTable(
                name: "Log_MenuGroups");

            migrationBuilder.DropTable(
                name: "Log_Notifications");

            migrationBuilder.DropTable(
                name: "Log_Positions");

            migrationBuilder.DropTable(
                name: "Log_Posts");

            migrationBuilder.DropTable(
                name: "Log_Sections");

            migrationBuilder.DropTable(
                name: "log_ShopGroups");

            migrationBuilder.DropTable(
                name: "Log_ShopPositionGroups");

            migrationBuilder.DropTable(
                name: "Log_Shops");

            migrationBuilder.DropTable(
                name: "Log_UserConnections");

            migrationBuilder.DropTable(
                name: "Log_UserMenuGroups");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PasswordHistories");

            migrationBuilder.DropTable(
                name: "UserMenuGroups");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DirectoryGroups");

            migrationBuilder.DropTable(
                name: "CategoryComplaints");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MenuGroups");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "ShopGroups");

            migrationBuilder.DropTable(
                name: "ShopPositionGroups");

            migrationBuilder.DropTable(
                name: "Department2s");

            migrationBuilder.DropTable(
                name: "Department1s");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "LocationGroups");
        }
    }
}
