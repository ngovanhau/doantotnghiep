CREATE TABLE "Dashboard"
(
    "Id" uuid NOT NULL,
    "building" int,
    "customer" int,
    "contract" int,
    "problem" int,
    "room" int,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "Dashboard_pkey" PRIMARY KEY ("Id")
)

