CREATE TABLE "contract"
(
    "Id" uuid NOT NULL,
    "contract_name" varchar(255),
    "room" varchar(255),
    "roomId" uuid,
    "start_day" timestamp with time zone,
    "end_day" timestamp with time zone,
    "billing_start_date" timestamp with time zone,
    "payment_term" int,
    "room_fee" int,
    "deposit" int,
    "CustomerId" uuid,
    "service" varchar(255),
    "clause" varchar(255),
    "image" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "contract_pkey" PRIMARY KEY ("Id")
)

