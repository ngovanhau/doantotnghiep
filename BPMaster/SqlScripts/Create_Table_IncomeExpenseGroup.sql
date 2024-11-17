CREATE TABLE "incomeexpensegroup"
(
    "Id" uuid NOT NULL,
    "date" timestamp with time zone,
    "amount" int,
    "transactiongroupid" uuid,
    "transactiongroupname" varchar(255),
    "paymentmethod" varchar(255),
    "contractid" uuid,
    "contractname" varchar(255),
    "note" varchar(255),
    "image" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "incomeexpensegroup_pkey" PRIMARY KEY ("Id")
)

