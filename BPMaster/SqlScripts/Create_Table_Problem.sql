CREATE TABLE "problem"
(
    "Id" uuid NOT NULL,
    "room_name" varchar(255),
    "problem" varchar(255),
    "decription" varchar(255),
    "image" varchar(255),
    "fatal_level" int,
    "status" int,
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "problem_pkey" PRIMARY KEY ("Id")
)

