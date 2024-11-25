CREATE TABLE "TransactionGroup"
(
    "Id" uuid NOT NULL,
    "type" int,
    "name" varchar(255),
    "image" varchar(255),
    "note" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "TransactionGroup_pkey" PRIMARY KEY ("Id")
)

