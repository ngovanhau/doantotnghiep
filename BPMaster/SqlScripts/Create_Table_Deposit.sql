CREATE TABLE "deposit"
(
    "Id" uuid NOT NULL,
    "deposit_amount" numeric,
    "room" varchar(255),
    "move_in_date" timestamp with time zone,
    "payment_method" varchar(255),
    "depositor" varchar(255),
    "image" varchar(255),
    "note" varchar(255),
    "status" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "deposit_pkey" PRIMARY KEY ("Id")
)

