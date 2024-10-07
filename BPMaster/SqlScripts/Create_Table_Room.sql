CREATE TABLE "room"
(
    "Id" uuid NOT NULL,
    "room_name" varchar(255),
    "room_price" numeric,
    "floor" int,
    "number_of_bedrooms" int,
    "number_of_living_rooms" int,
    "acreage" numeric,
    "limited_occupancy" int,
    "deposit" numeric,
    "renter" int,
    "service" varchar(255),
    "image" varchar(255),
    "utilities" varchar(255),
    "interior" varchar(255),
    "describe" varchar(255),
    "note" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "room_pkey" PRIMARY KEY ("Id")
)

