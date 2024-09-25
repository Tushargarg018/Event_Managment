using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EM.Data.Migrations
{
    /// <inheritdoc />
    public partial class tushar_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "performer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    bio = table.Column<string>(type: "text", nullable: false),
                    profile = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_performer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    country_code = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.id);
                    table.ForeignKey(
                        name: "FK_currency_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                    table.ForeignKey(
                        name: "FK_state_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.id);
                    table.ForeignKey(
                        name: "FK_city_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tax_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    tax_details = table.Column<JsonDocument>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_configuration", x => x.id);
                    table.ForeignKey(
                        name: "FK_tax_configuration_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tax_configuration_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "venue",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    max_capacity = table.Column<int>(type: "integer", nullable: false),
                    address_line1 = table.Column<string>(type: "text", nullable: false),
                    address_line2 = table.Column<string>(type: "text", nullable: true),
                    zip_code = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venue", x => x.id);
                    table.ForeignKey(
                        name: "FK_venue_city_city",
                        column: x => x.city,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_venue_state_state",
                        column: x => x.state,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    base_price = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    organizer_id = table.Column<int>(type: "integer", nullable: false),
                    performer_id = table.Column<int>(type: "integer", nullable: false),
                    venue_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    start_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    flag = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_organizer_organizer_id",
                        column: x => x.organizer_id,
                        principalTable: "organizer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_performer_performer_id",
                        column: x => x.performer_id,
                        principalTable: "performer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_venue_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_document",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_document", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_document_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_offer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_offers = table.Column<int>(type: "integer", nullable: false),
                    group_size = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_offer", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_offer_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_ticket_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_ticket_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_ticket_category_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "India" });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "id", "country_code", "country_id", "currency_code", "symbol" },
                values: new object[] { 1, "IN", 1, "INR", "₹" });

            migrationBuilder.InsertData(
                table: "state",
                columns: new[] { "id", "country_id", "name" },
                values: new object[,]
                {
                    { 1, 1, "Andhra Pradesh" },
                    { 2, 1, "Assam" },
                    { 3, 1, "Arunachal Pradesh" },
                    { 4, 1, "Bihar" },
                    { 5, 1, "Gujrat" },
                    { 6, 1, "Haryana" },
                    { 7, 1, "Himachal Pradesh" },
                    { 8, 1, "Jammu & Kashmir" },
                    { 9, 1, "Karnataka" },
                    { 10, 1, "Kerala" },
                    { 11, 1, "Madhya Pradesh" },
                    { 12, 1, "Maharashtra" },
                    { 13, 1, "Manipur" },
                    { 14, 1, "Meghalaya" },
                    { 15, 1, "Mizoram" },
                    { 16, 1, "Nagaland" },
                    { 17, 1, "Orissa" },
                    { 18, 1, "Punjab" },
                    { 19, 1, "Rajasthan" },
                    { 20, 1, "Sikkim" },
                    { 21, 1, "Tamil Nadu" },
                    { 22, 1, "Tripura" },
                    { 23, 1, "Uttar Pradesh" },
                    { 24, 1, "West Bengal" },
                    { 25, 1, "Delhi" },
                    { 26, 1, "Goa" },
                    { 27, 1, "Pondichery" },
                    { 28, 1, "Lakshadweep" },
                    { 29, 1, "Daman & Diu" },
                    { 30, 1, "Dadra & Nagar Haveli" },
                    { 31, 1, "Chandigarh" },
                    { 32, 1, "Andaman & Nicobar" },
                    { 33, 1, "Uttarakhand" },
                    { 34, 1, "Jharkhand" },
                    { 35, 1, "Chhattisgarh" }
                });

            migrationBuilder.InsertData(
                table: "city",
                columns: new[] { "id", "name", "state_id" },
                values: new object[,]
                {
                    { 1, "North and Middle Andaman", 32 },
                    { 2, "South Andaman", 32 },
                    { 3, "Nicobar", 32 },
                    { 4, "Adilabad", 1 },
                    { 5, "Anantapur", 1 },
                    { 6, "Chittoor", 1 },
                    { 7, "East Godavari", 1 },
                    { 8, "Guntur", 1 },
                    { 9, "Hyderabad", 1 },
                    { 10, "Kadapa", 1 },
                    { 11, "Karimnagar", 1 },
                    { 12, "Khammam", 1 },
                    { 13, "Krishna", 1 },
                    { 14, "Kurnool", 1 },
                    { 15, "Mahbubnagar", 1 },
                    { 16, "Medak", 1 },
                    { 17, "Nalgonda", 1 },
                    { 18, "Nellore", 1 },
                    { 19, "Nizamabad", 1 },
                    { 20, "Prakasam", 1 },
                    { 21, "Rangareddi", 1 },
                    { 22, "Srikakulam", 1 },
                    { 23, "Vishakhapatnam", 1 },
                    { 24, "Vizianagaram", 1 },
                    { 25, "Warangal", 1 },
                    { 26, "West Godavari", 1 },
                    { 27, "Anjaw", 3 },
                    { 28, "Changlang", 3 },
                    { 29, "East Kameng", 3 },
                    { 30, "Lohit", 3 },
                    { 31, "Lower Subansiri", 3 },
                    { 32, "Papum Pare", 3 },
                    { 33, "Tirap", 3 },
                    { 34, "Dibang Valley", 3 },
                    { 35, "Upper Subansiri", 3 },
                    { 36, "West Kameng", 3 },
                    { 37, "Barpeta", 2 },
                    { 38, "Bongaigaon", 2 },
                    { 39, "Cachar", 2 },
                    { 40, "Darrang", 2 },
                    { 41, "Dhemaji", 2 },
                    { 42, "Dhubri", 2 },
                    { 43, "Dibrugarh", 2 },
                    { 44, "Goalpara", 2 },
                    { 45, "Golaghat", 2 },
                    { 46, "Hailakandi", 2 },
                    { 47, "Jorhat", 2 },
                    { 48, "Karbi Anglong", 2 },
                    { 49, "Karimganj", 2 },
                    { 50, "Kokrajhar", 2 },
                    { 51, "Lakhimpur", 2 },
                    { 52, "Marigaon", 2 },
                    { 53, "Nagaon", 2 },
                    { 54, "Nalbari", 2 },
                    { 55, "North Cachar Hills", 2 },
                    { 56, "Sibsagar", 2 },
                    { 57, "Sonitpur", 2 },
                    { 58, "Tinsukia", 2 },
                    { 59, "Araria", 4 },
                    { 60, "Aurangabad", 4 },
                    { 61, "Banka", 4 },
                    { 62, "Begusarai", 4 },
                    { 63, "Bhagalpur", 4 },
                    { 64, "Bhojpur", 4 },
                    { 65, "Buxar", 4 },
                    { 66, "Darbhanga", 4 },
                    { 67, "Purba Champaran", 4 },
                    { 68, "Gaya", 4 },
                    { 69, "Gopalganj", 4 },
                    { 70, "Jamui", 4 },
                    { 71, "Jehanabad", 4 },
                    { 72, "Khagaria", 4 },
                    { 73, "Kishanganj", 4 },
                    { 74, "Kaimur", 4 },
                    { 75, "Katihar", 4 },
                    { 76, "Lakhisarai", 4 },
                    { 77, "Madhubani", 4 },
                    { 78, "Munger", 4 },
                    { 79, "Madhepura", 4 },
                    { 80, "Muzaffarpur", 4 },
                    { 81, "Nalanda", 4 },
                    { 82, "Nawada", 4 },
                    { 83, "Patna", 4 },
                    { 84, "Purnia", 4 },
                    { 85, "Rohtas", 4 },
                    { 86, "Saharsa", 4 },
                    { 87, "Samastipur", 4 },
                    { 88, "Sheohar", 4 },
                    { 89, "Sheikhpura", 4 },
                    { 90, "Saran", 4 },
                    { 91, "Sitamarhi", 4 },
                    { 92, "Supaul", 4 },
                    { 93, "Siwan", 4 },
                    { 94, "Vaishali", 4 },
                    { 95, "Pashchim Champaran", 4 },
                    { 96, "Bastar", 35 },
                    { 97, "Bilaspur", 35 },
                    { 98, "Dantewada", 35 },
                    { 99, "Dhamtari", 35 },
                    { 100, "Durg", 35 },
                    { 101, "Jashpur", 35 },
                    { 102, "Janjgir-Champa", 35 },
                    { 103, "Korba", 35 },
                    { 104, "Koriya", 35 },
                    { 105, "Kanker", 35 },
                    { 106, "Kawardha", 35 },
                    { 107, "Mahasamund", 35 },
                    { 108, "Raigarh", 35 },
                    { 109, "Rajnandgaon", 35 },
                    { 110, "Raipur", 35 },
                    { 111, "Surguja", 35 },
                    { 112, "Diu", 29 },
                    { 113, "Daman", 29 },
                    { 114, "Central Delhi", 25 },
                    { 115, "East Delhi", 25 },
                    { 116, "New Delhi", 25 },
                    { 117, "North Delhi", 25 },
                    { 118, "North East Delhi", 25 },
                    { 119, "North West Delhi", 25 },
                    { 120, "South Delhi", 25 },
                    { 121, "South West Delhi", 25 },
                    { 122, "West Delhi", 25 },
                    { 123, "North Goa", 26 },
                    { 124, "South Goa", 26 },
                    { 125, "Ahmedabad", 5 },
                    { 126, "Amreli District", 5 },
                    { 127, "Anand", 5 },
                    { 128, "Banaskantha", 5 },
                    { 129, "Bharuch", 5 },
                    { 130, "Bhavnagar", 5 },
                    { 131, "Dahod", 5 },
                    { 132, "The Dangs", 5 },
                    { 133, "Gandhinagar", 5 },
                    { 134, "Jamnagar", 5 },
                    { 135, "Junagadh", 5 },
                    { 136, "Kutch", 5 },
                    { 137, "Kheda", 5 },
                    { 138, "Mehsana", 5 },
                    { 139, "Narmada", 5 },
                    { 140, "Navsari", 5 },
                    { 141, "Patan", 5 },
                    { 142, "Panchmahal", 5 },
                    { 143, "Porbandar", 5 },
                    { 144, "Rajkot", 5 },
                    { 145, "Sabarkantha", 5 },
                    { 146, "Surendranagar", 5 },
                    { 147, "Surat", 5 },
                    { 148, "Vadodara", 5 },
                    { 149, "Valsad", 5 },
                    { 150, "Ambala", 6 },
                    { 151, "Bhiwani", 6 },
                    { 152, "Faridabad", 6 },
                    { 153, "Fatehabad", 6 },
                    { 154, "Gurgaon", 6 },
                    { 155, "Hissar", 6 },
                    { 156, "Jhajjar", 6 },
                    { 157, "Jind", 6 },
                    { 158, "Karnal", 6 },
                    { 159, "Kaithal", 6 },
                    { 160, "Kurukshetra", 6 },
                    { 161, "Mahendragarh", 6 },
                    { 162, "Mewat", 6 },
                    { 163, "Panchkula", 6 },
                    { 164, "Panipat", 6 },
                    { 165, "Rewari", 6 },
                    { 166, "Rohtak", 6 },
                    { 167, "Sirsa", 6 },
                    { 168, "Sonepat", 6 },
                    { 169, "Yamuna Nagar", 6 },
                    { 170, "Palwal", 6 },
                    { 171, "Bilaspur", 7 },
                    { 172, "Chamba", 7 },
                    { 173, "Hamirpur", 7 },
                    { 174, "Kangra", 7 },
                    { 175, "Kinnaur", 7 },
                    { 176, "Kulu", 7 },
                    { 177, "Lahaul and Spiti", 7 },
                    { 178, "Mandi", 7 },
                    { 179, "Shimla", 7 },
                    { 180, "Sirmaur", 7 },
                    { 181, "Solan", 7 },
                    { 182, "Una", 7 },
                    { 183, "Anantnag", 8 },
                    { 184, "Badgam", 8 },
                    { 185, "Bandipore", 8 },
                    { 186, "Baramula", 8 },
                    { 187, "Doda", 8 },
                    { 188, "Jammu", 8 },
                    { 189, "Kargil", 8 },
                    { 190, "Kathua", 8 },
                    { 191, "Kupwara", 8 },
                    { 192, "Leh", 8 },
                    { 193, "Poonch", 8 },
                    { 194, "Pulwama", 8 },
                    { 195, "Rajauri", 8 },
                    { 196, "Srinagar", 8 },
                    { 197, "Samba", 8 },
                    { 198, "Udhampur", 8 },
                    { 199, "Bokaro", 34 },
                    { 200, "Chatra", 34 },
                    { 201, "Deoghar", 34 },
                    { 202, "Dhanbad", 34 },
                    { 203, "Dumka", 34 },
                    { 204, "Purba Singhbhum", 34 },
                    { 205, "Garhwa", 34 },
                    { 206, "Giridih", 34 },
                    { 207, "Godda", 34 },
                    { 208, "Gumla", 34 },
                    { 209, "Hazaribagh", 34 },
                    { 210, "Koderma", 34 },
                    { 211, "Lohardaga", 34 },
                    { 212, "Pakur", 34 },
                    { 213, "Palamu", 34 },
                    { 214, "Ranchi", 34 },
                    { 215, "Sahibganj", 34 },
                    { 216, "Seraikela and Kharsawan", 34 },
                    { 217, "Pashchim Singhbhum", 34 },
                    { 218, "Ramgarh", 34 },
                    { 219, "Bidar", 9 },
                    { 220, "Belgaum", 9 },
                    { 221, "Bijapur", 9 },
                    { 222, "Bagalkot", 9 },
                    { 223, "Bellary", 9 },
                    { 224, "Bangalore Rural District", 9 },
                    { 225, "Bangalore Urban District", 9 },
                    { 226, "Chamarajnagar", 9 },
                    { 227, "Chikmagalur", 9 },
                    { 228, "Chitradurga", 9 },
                    { 229, "Davanagere", 9 },
                    { 230, "Dharwad", 9 },
                    { 231, "Dakshina Kannada", 9 },
                    { 232, "Gadag", 9 },
                    { 233, "Gulbarga", 9 },
                    { 234, "Hassan", 9 },
                    { 235, "Haveri District", 9 },
                    { 236, "Kodagu", 9 },
                    { 237, "Kolar", 9 },
                    { 238, "Koppal", 9 },
                    { 239, "Mandya", 9 },
                    { 240, "Mysore", 9 },
                    { 241, "Raichur", 9 },
                    { 242, "Shimoga", 9 },
                    { 243, "Tumkur", 9 },
                    { 244, "Udupi", 9 },
                    { 245, "Uttara Kannada", 9 },
                    { 246, "Ramanagara", 9 },
                    { 247, "Chikballapur", 9 },
                    { 248, "Yadagiri", 9 },
                    { 249, "Alappuzha", 10 },
                    { 250, "Ernakulam", 10 },
                    { 251, "Idukki", 10 },
                    { 252, "Kollam", 10 },
                    { 253, "Kannur", 10 },
                    { 254, "Kasaragod", 10 },
                    { 255, "Kottayam", 10 },
                    { 256, "Kozhikode", 10 },
                    { 257, "Malappuram", 10 },
                    { 258, "Palakkad", 10 },
                    { 259, "Pathanamthitta", 10 },
                    { 260, "Thrissur", 10 },
                    { 261, "Thiruvananthapuram", 10 },
                    { 262, "Wayanad", 10 },
                    { 263, "Alirajpur", 11 },
                    { 264, "Anuppur", 11 },
                    { 265, "Ashok Nagar", 11 },
                    { 266, "Balaghat", 11 },
                    { 267, "Barwani", 11 },
                    { 268, "Betul", 11 },
                    { 269, "Bhind", 11 },
                    { 270, "Bhopal", 11 },
                    { 271, "Burhanpur", 11 },
                    { 272, "Chhatarpur", 11 },
                    { 273, "Chhindwara", 11 },
                    { 274, "Damoh", 11 },
                    { 275, "Datia", 11 },
                    { 276, "Dewas", 11 },
                    { 277, "Dhar", 11 },
                    { 278, "Dindori", 11 },
                    { 279, "Guna", 11 },
                    { 280, "Gwalior", 11 },
                    { 281, "Harda", 11 },
                    { 282, "Hoshangabad", 11 },
                    { 283, "Indore", 11 },
                    { 284, "Jabalpur", 11 },
                    { 285, "Jhabua", 11 },
                    { 286, "Katni", 11 },
                    { 287, "Khandwa", 11 },
                    { 288, "Khargone", 11 },
                    { 289, "Mandla", 11 },
                    { 290, "Mandsaur", 11 },
                    { 291, "Morena", 11 },
                    { 292, "Narsinghpur", 11 },
                    { 293, "Neemuch", 11 },
                    { 294, "Panna", 11 },
                    { 295, "Rewa", 11 },
                    { 296, "Rajgarh", 11 },
                    { 297, "Ratlam", 11 },
                    { 298, "Raisen", 11 },
                    { 299, "Sagar", 11 },
                    { 300, "Satna", 11 },
                    { 301, "Sehore", 11 },
                    { 302, "Seoni", 11 },
                    { 303, "Shahdol", 11 },
                    { 304, "Shajapur", 11 },
                    { 305, "Sheopur", 11 },
                    { 306, "Shivpuri", 11 },
                    { 307, "Sidhi", 11 },
                    { 308, "Singrauli", 11 },
                    { 309, "Tikamgarh", 11 },
                    { 310, "Ujjain", 11 },
                    { 311, "Umaria", 11 },
                    { 312, "Vidisha", 11 },
                    { 313, "Ahmednagar", 12 },
                    { 314, "Akola", 12 },
                    { 315, "Amrawati", 12 },
                    { 316, "Aurangabad", 12 },
                    { 317, "Bhandara", 12 },
                    { 318, "Beed", 12 },
                    { 319, "Buldhana", 12 },
                    { 320, "Chandrapur", 12 },
                    { 321, "Dhule", 12 },
                    { 322, "Gadchiroli", 12 },
                    { 323, "Gondiya", 12 },
                    { 324, "Hingoli", 12 },
                    { 325, "Jalgaon", 12 },
                    { 326, "Jalna", 12 },
                    { 327, "Kolhapur", 12 },
                    { 328, "Latur", 12 },
                    { 329, "Mumbai City", 12 },
                    { 330, "Mumbai suburban", 12 },
                    { 331, "Nandurbar", 12 },
                    { 332, "Nanded", 12 },
                    { 333, "Nagpur", 12 },
                    { 334, "Nashik", 12 },
                    { 335, "Osmanabad", 12 },
                    { 336, "Parbhani", 12 },
                    { 337, "Pune", 12 },
                    { 338, "Raigad", 12 },
                    { 339, "Ratnagiri", 12 },
                    { 340, "Sindhudurg", 12 },
                    { 341, "Sangli", 12 },
                    { 342, "Solapur", 12 },
                    { 343, "Satara", 12 },
                    { 344, "Thane", 12 },
                    { 345, "Wardha", 12 },
                    { 346, "Washim", 12 },
                    { 347, "Yavatmal", 12 },
                    { 348, "Bishnupur", 13 },
                    { 349, "Churachandpur", 13 },
                    { 350, "Chandel", 13 },
                    { 351, "Imphal East", 13 },
                    { 352, "Senapati", 13 },
                    { 353, "Tamenglong", 13 },
                    { 354, "Thoubal", 13 },
                    { 355, "Ukhrul", 13 },
                    { 356, "Imphal West", 13 },
                    { 357, "East Garo Hills", 14 },
                    { 358, "East Khasi Hills", 14 },
                    { 359, "Jaintia Hills", 14 },
                    { 360, "Ri-Bhoi", 14 },
                    { 361, "South Garo Hills", 14 },
                    { 362, "West Garo Hills", 14 },
                    { 363, "West Khasi Hills", 14 },
                    { 364, "Aizawl", 15 },
                    { 365, "Champhai", 15 },
                    { 366, "Kolasib", 15 },
                    { 367, "Lawngtlai", 15 },
                    { 368, "Lunglei", 15 },
                    { 369, "Mamit", 15 },
                    { 370, "Saiha", 15 },
                    { 371, "Serchhip", 15 },
                    { 372, "Dimapur", 16 },
                    { 373, "Kohima", 16 },
                    { 374, "Mokokchung", 16 },
                    { 375, "Mon", 16 },
                    { 376, "Phek", 16 },
                    { 377, "Tuensang", 16 },
                    { 378, "Wokha", 16 },
                    { 379, "Zunheboto", 16 },
                    { 380, "Angul", 17 },
                    { 381, "Boudh", 17 },
                    { 382, "Bhadrak", 17 },
                    { 383, "Bolangir", 17 },
                    { 384, "Bargarh", 17 },
                    { 385, "Baleswar", 17 },
                    { 386, "Cuttack", 17 },
                    { 387, "Debagarh", 17 },
                    { 388, "Dhenkanal", 17 },
                    { 389, "Ganjam", 17 },
                    { 390, "Gajapati", 17 },
                    { 391, "Jharsuguda", 17 },
                    { 392, "Jajapur", 17 },
                    { 393, "Jagatsinghpur", 17 },
                    { 394, "Khordha", 17 },
                    { 395, "Kendujhar", 17 },
                    { 396, "Kalahandi", 17 },
                    { 397, "Kandhamal", 17 },
                    { 398, "Koraput", 17 },
                    { 399, "Kendrapara", 17 },
                    { 400, "Malkangiri", 17 },
                    { 401, "Mayurbhanj", 17 },
                    { 402, "Nabarangpur", 17 },
                    { 403, "Nuapada", 17 },
                    { 404, "Nayagarh", 17 },
                    { 405, "Puri", 17 },
                    { 406, "Rayagada", 17 },
                    { 407, "Sambalpur", 17 },
                    { 408, "Subarnapur", 17 },
                    { 409, "Sundargarh", 17 },
                    { 410, "Karaikal", 27 },
                    { 411, "Mahe", 27 },
                    { 412, "Puducherry", 27 },
                    { 413, "Yanam", 27 },
                    { 414, "Amritsar", 18 },
                    { 415, "Bathinda", 18 },
                    { 416, "Firozpur", 18 },
                    { 417, "Faridkot", 18 },
                    { 418, "Fatehgarh Sahib", 18 },
                    { 419, "Gurdaspur", 18 },
                    { 420, "Hoshiarpur", 18 },
                    { 421, "Jalandhar", 18 },
                    { 422, "Kapurthala", 18 },
                    { 423, "Ludhiana", 18 },
                    { 424, "Mansa", 18 },
                    { 425, "Moga", 18 },
                    { 426, "Mukatsar", 18 },
                    { 427, "Nawan Shehar", 18 },
                    { 428, "Patiala", 18 },
                    { 429, "Rupnagar", 18 },
                    { 430, "Sangrur", 18 },
                    { 431, "Ajmer", 19 },
                    { 432, "Alwar", 19 },
                    { 433, "Bikaner", 19 },
                    { 434, "Barmer", 19 },
                    { 435, "Banswara", 19 },
                    { 436, "Bharatpur", 19 },
                    { 437, "Baran", 19 },
                    { 438, "Bundi", 19 },
                    { 439, "Bhilwara", 19 },
                    { 440, "Churu", 19 },
                    { 441, "Chittorgarh", 19 },
                    { 442, "Dausa", 19 },
                    { 443, "Dholpur", 19 },
                    { 444, "Dungapur", 19 },
                    { 445, "Ganganagar", 19 },
                    { 446, "Hanumangarh", 19 },
                    { 447, "Juhnjhunun", 19 },
                    { 448, "Jalore", 19 },
                    { 449, "Jodhpur", 19 },
                    { 450, "Jaipur", 19 },
                    { 451, "Jaisalmer", 19 },
                    { 452, "Jhalawar", 19 },
                    { 453, "Karauli", 19 },
                    { 454, "Kota", 19 },
                    { 455, "Nagaur", 19 },
                    { 456, "Pali", 19 },
                    { 457, "Pratapgarh", 19 },
                    { 458, "Rajsamand", 19 },
                    { 459, "Sikar", 19 },
                    { 460, "Sawai Madhopur", 19 },
                    { 461, "Sirohi", 19 },
                    { 462, "Tonk", 19 },
                    { 463, "Udaipur", 19 },
                    { 464, "East Sikkim", 20 },
                    { 465, "North Sikkim", 20 },
                    { 466, "South Sikkim", 20 },
                    { 467, "West Sikkim", 20 },
                    { 468, "Ariyalur", 21 },
                    { 469, "Chennai", 21 },
                    { 470, "Coimbatore", 21 },
                    { 471, "Cuddalore", 21 },
                    { 472, "Dharmapuri", 21 },
                    { 473, "Dindigul", 21 },
                    { 474, "Erode", 21 },
                    { 475, "Kanchipuram", 21 },
                    { 476, "Kanyakumari", 21 },
                    { 477, "Karur", 21 },
                    { 478, "Madurai", 21 },
                    { 479, "Nagapattinam", 21 },
                    { 480, "The Nilgiris", 21 },
                    { 481, "Namakkal", 21 },
                    { 482, "Perambalur", 21 },
                    { 483, "Pudukkottai", 21 },
                    { 484, "Ramanathapuram", 21 },
                    { 485, "Salem", 21 },
                    { 486, "Sivagangai", 21 },
                    { 487, "Tiruppur", 21 },
                    { 488, "Tiruchirappalli", 21 },
                    { 489, "Theni", 21 },
                    { 490, "Tirunelveli", 21 },
                    { 491, "Thanjavur", 21 },
                    { 492, "Thoothukudi", 21 },
                    { 493, "Thiruvallur", 21 },
                    { 494, "Thiruvarur", 21 },
                    { 495, "Tiruvannamalai", 21 },
                    { 496, "Vellore", 21 },
                    { 497, "Villupuram", 21 },
                    { 498, "Dhalai", 22 },
                    { 499, "North Tripura", 22 },
                    { 500, "South Tripura", 22 },
                    { 501, "West Tripura", 22 },
                    { 502, "Almora", 33 },
                    { 503, "Bageshwar", 33 },
                    { 504, "Chamoli", 33 },
                    { 505, "Champawat", 33 },
                    { 506, "Dehradun", 33 },
                    { 507, "Haridwar", 33 },
                    { 508, "Nainital", 33 },
                    { 509, "Pauri Garhwal", 33 },
                    { 510, "Pithoragharh", 33 },
                    { 511, "Rudraprayag", 33 },
                    { 512, "Tehri Garhwal", 33 },
                    { 513, "Udham Singh Nagar", 33 },
                    { 514, "Uttarkashi", 33 },
                    { 515, "Agra", 23 },
                    { 516, "Allahabad", 23 },
                    { 517, "Aligarh", 23 },
                    { 518, "Ambedkar Nagar", 23 },
                    { 519, "Auraiya", 23 },
                    { 520, "Azamgarh", 23 },
                    { 521, "Barabanki", 23 },
                    { 522, "Badaun", 23 },
                    { 523, "Bagpat", 23 },
                    { 524, "Bahraich", 23 },
                    { 525, "Bijnor", 23 },
                    { 526, "Ballia", 23 },
                    { 527, "Banda", 23 },
                    { 528, "Balrampur", 23 },
                    { 529, "Bareilly", 23 },
                    { 530, "Basti", 23 },
                    { 531, "Bulandshahr", 23 },
                    { 532, "Chandauli", 23 },
                    { 533, "Chitrakoot", 23 },
                    { 534, "Deoria", 23 },
                    { 535, "Etah", 23 },
                    { 536, "Kanshiram Nagar", 23 },
                    { 537, "Etawah", 23 },
                    { 538, "Firozabad", 23 },
                    { 539, "Farrukhabad", 23 },
                    { 540, "Fatehpur", 23 },
                    { 541, "Faizabad", 23 },
                    { 542, "Gautam Buddha Nagar", 23 },
                    { 543, "Gonda", 23 },
                    { 544, "Ghazipur", 23 },
                    { 545, "Gorkakhpur", 23 },
                    { 546, "Ghaziabad", 23 },
                    { 547, "Hamirpur", 23 },
                    { 548, "Hardoi", 23 },
                    { 549, "Mahamaya Nagar", 23 },
                    { 550, "Jhansi", 23 },
                    { 551, "Jalaun", 23 },
                    { 552, "Jyotiba Phule Nagar", 23 },
                    { 553, "Jaunpur District", 23 },
                    { 554, "Kanpur Dehat", 23 },
                    { 555, "Kannauj", 23 },
                    { 556, "Kanpur Nagar", 23 },
                    { 557, "Kaushambi", 23 },
                    { 558, "Kushinagar", 23 },
                    { 559, "Lalitpur", 23 },
                    { 560, "Lakhimpur Kheri", 23 },
                    { 561, "Lucknow", 23 },
                    { 562, "Mau", 23 },
                    { 563, "Meerut", 23 },
                    { 564, "Maharajganj", 23 },
                    { 565, "Mahoba", 23 },
                    { 566, "Mirzapur", 23 },
                    { 567, "Moradabad", 23 },
                    { 568, "Mainpuri", 23 },
                    { 569, "Mathura", 23 },
                    { 570, "Muzaffarnagar", 23 },
                    { 571, "Pilibhit", 23 },
                    { 572, "Pratapgarh", 23 },
                    { 573, "Rampur", 23 },
                    { 574, "Rae Bareli", 23 },
                    { 575, "Saharanpur", 23 },
                    { 576, "Sitapur", 23 },
                    { 577, "Shahjahanpur", 23 },
                    { 578, "Sant Kabir Nagar", 23 },
                    { 579, "Siddharthnagar", 23 },
                    { 580, "Sonbhadra", 23 },
                    { 581, "Sant Ravidas Nagar", 23 },
                    { 582, "Sultanpur", 23 },
                    { 583, "Shravasti", 23 },
                    { 584, "Unnao", 23 },
                    { 585, "Varanasi", 23 },
                    { 586, "Birbhum", 24 },
                    { 587, "Bankura", 24 },
                    { 588, "Bardhaman", 24 },
                    { 589, "Darjeeling", 24 },
                    { 590, "Dakshin Dinajpur", 24 },
                    { 591, "Hooghly", 24 },
                    { 592, "Howrah", 24 },
                    { 593, "Jalpaiguri", 24 },
                    { 594, "Cooch Behar", 24 },
                    { 595, "Kolkata", 24 },
                    { 596, "Malda", 24 },
                    { 597, "Midnapore", 24 },
                    { 598, "Murshidabad", 24 },
                    { 599, "Nadia", 24 },
                    { 600, "North 24 Parganas", 24 },
                    { 601, "South 24 Parganas", 24 },
                    { 602, "Purulia", 24 },
                    { 603, "Uttar Dinajpur", 24 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_state_id",
                table: "city",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_currency_country_id",
                table: "currency",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_organizer_id",
                table: "event",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_performer_id",
                table: "event",
                column: "performer_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_venue_id",
                table: "event",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_document_event_id",
                table: "event_document",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_offer_event_id",
                table: "event_offer",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_ticket_category_event_id",
                table: "event_ticket_category",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_state_country_id",
                table: "state",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_tax_configuration_country_id",
                table: "tax_configuration",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_tax_configuration_state_id",
                table: "tax_configuration",
                column: "state_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_venue_city",
                table: "venue",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_venue_state",
                table: "venue",
                column: "state");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "event_document");

            migrationBuilder.DropTable(
                name: "event_offer");

            migrationBuilder.DropTable(
                name: "event_ticket_category");

            migrationBuilder.DropTable(
                name: "tax_configuration");

            migrationBuilder.DropTable(
                name: "event");

            migrationBuilder.DropTable(
                name: "organizer");

            migrationBuilder.DropTable(
                name: "performer");

            migrationBuilder.DropTable(
                name: "venue");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
