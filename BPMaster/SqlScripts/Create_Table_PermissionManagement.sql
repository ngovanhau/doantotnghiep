CREATE TABLE "permission"
(
    "Id" uuid NOT NULL,
    "Username" varchar(255),
    "UserId" uuid,
    "BuildingId" uuid,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "permission_pkey" PRIMARY KEY ("Id")
)

