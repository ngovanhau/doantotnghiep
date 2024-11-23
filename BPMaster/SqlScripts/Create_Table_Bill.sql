CREATE TABLE "Bill"
(
    "Id" uuid NOT NULL,
    "bill_name" varchar(255),
    "status" int,
    "status_payment" int,
    "building_id" uuid,
    "customer_name" varchar(255),
    "customer_id" uuid,
    "date" timestamp with time zone,
    "roomid" uuid,
    "roomname" varchar(255),
    "payment_date" timestamp with time zone,
    "due_date" timestamp with time zone,
    "cost_room" int,
    "cost_service" int,
    "total_amount" int,
    "penalty_amount" int,
    "discount" int,
    "final_amount" int,
    "transaction_id" varchar(255),
    "note" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "Bill_pkey" PRIMARY KEY ("Id")
)

