CREATE TABLE "Building"
(
    "Id" uuid NOT NULL,
    "building_name" varchar(255),
    "number_of_floors" varchar(255),
    "rental_costs" varchar(255),
    "description" varchar(255),
    "address" varchar(255),
    "city" varchar(255),
    "district" varchar(255),
    "payment_date" int,
    "advance_notice" int,
    "payment_time" int,
    "payment_timeout" int,
    "management" varchar(255),
    "utilities" varchar(255),
    "building_note" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "Building_pkey" PRIMARY KEY ("Id")
)

