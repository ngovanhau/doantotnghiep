CREATE TABLE "bookings"
(
    "Id" uuid NOT NULL,
    "roomid" uuid,
    "customername" varchar(255),
    "phone" varchar(255),
    "email" varchar(255),
    "Date" timestamp with time zone,
    "status" int,
    "note" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "bookings_pkey" PRIMARY KEY ("Id")
)

