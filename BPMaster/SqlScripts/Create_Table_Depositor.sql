CREATE TABLE "depositor"
(
    "Id" uuid NOT NULL,
    "depositor_name" varchar(255),
    "phone_number" varchar(255),
    "email" varchar(255),
    "ID_number" int,
    "address" varchar(255),
    "image" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "depositor_pkey" PRIMARY KEY ("Id")
)

