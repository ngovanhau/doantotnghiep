CREATE TABLE "deposit"
(
    "Id" uuid NOT NULL,
    "deposit_amount" numeric,
    "roomid" uuid,
    "roomname" varchar(255),
    "move_in_date" timestamp with time zone,
    "payment_method" varchar(255),
    "customerid" uuid,
    "customername" varchar(255),
    "note" varchar(255),
    "status" int,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "deposit_pkey" PRIMARY KEY ("Id")
)

