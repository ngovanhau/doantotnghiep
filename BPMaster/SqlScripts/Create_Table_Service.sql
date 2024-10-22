CREATE TABLE "service"
(
    "Id" uuid NOT NULL,
    "service_name" varchar(255),
    "collect_fees" varchar(255),
    "unitMeasure" varchar(255),
    "service_cost" int,
    "image" varchar(255),
    "note" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "service_pkey" PRIMARY KEY ("Id")
)

