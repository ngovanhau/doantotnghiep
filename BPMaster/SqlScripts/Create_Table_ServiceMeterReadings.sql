CREATE TABLE "ServiceMeterReadings"
(
    "Id" uuid NOT NULL,
    "building_name" varchar(255),
    "building_id" uuid,
    "room_name" varchar(255),
    "room_id" uuid,
    "recorded_by" varchar(255),
    "record_date" timestamp with time zone,
    "electricity_old" numeric,
    "electricity_new" numeric,
    "electricity_price" numeric,
    "water_old" numeric,
    "water_new" numeric,
    "water_price" numeric,
    "confirmation_status" boolean,
    "total_amount" numeric,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "ServiceMeterReadings_pkey" PRIMARY KEY ("Id")
)

