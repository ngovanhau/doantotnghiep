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
    "payment_date" timestamp with time zone,
    "advance_notice" int,
    "payment_time" timestamp with time zone,
    "payment_timeout" timestamp with time zone,
    "management" varchar(255),
    "fee_based_service" varchar(255),
    "free_service" varchar(255),
    "utilities" varchar(255),
    "building_note" varchar(255),
    "deleted" int,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "Building_pkey" PRIMARY KEY ("Id")
)

