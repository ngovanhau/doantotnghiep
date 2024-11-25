CREATE TABLE "customer"
(
    "Id" uuid NOT NULL,
    "customer_name" varchar(255),
    "phone_number" varchar(255),
    "choose_room" uuid,
    "email" varchar(255),
    "date_of_birth" timestamp with time zone,
    "CCCD" varchar(255),
    "date_of_issue" timestamp with time zone,
    "place_of_issue" varchar(255),
    "address" varchar(255),
    "UserId" uuid,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "customer_pkey" PRIMARY KEY ("Id")
)

