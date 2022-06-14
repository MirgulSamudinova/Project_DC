﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Genders" (
    id_gender integer GENERATED BY DEFAULT AS IDENTITY,
    gender text NOT NULL,
    CONSTRAINT "PK_Genders" PRIMARY KEY (id_gender)
);

CREATE TABLE "Positions" (
    "Id_position" integer GENERATED BY DEFAULT AS IDENTITY,
    position text NOT NULL,
    CONSTRAINT "PK_Positions" PRIMARY KEY ("Id_position")
);

CREATE TABLE "Staffs" (
    id_staff integer GENERATED BY DEFAULT AS IDENTITY,
    sure_name text NOT NULL,
    first_name text NOT NULL,
    middle_name text NOT NULL,
    id_gender integer NULL,
    id_position integer NULL,
    birth_date date NOT NULL,
    mobile_number integer NOT NULL,
    e_mail text NOT NULL,
    CONSTRAINT "PK_Staffs" PRIMARY KEY (id_staff),
    CONSTRAINT "FK_Staffs_Genders_id_gender" FOREIGN KEY (id_gender) REFERENCES "Genders" (id_gender),
    CONSTRAINT "FK_Staffs_Positions_id_position" FOREIGN KEY (id_position) REFERENCES "Positions" ("Id_position")
);

CREATE INDEX "IX_Staffs_id_gender" ON "Staffs" (id_gender);

CREATE INDEX "IX_Staffs_id_position" ON "Staffs" (id_position);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220520055540_DBContext_ver', '6.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE "Staffs" ALTER COLUMN birth_date TYPE timestamp with time zone;

CREATE TABLE "Rooms" (
    "RoomsId" integer GENERATED BY DEFAULT AS IDENTITY,
    "RoomsName" text NOT NULL,
    CONSTRAINT "PK_Rooms" PRIMARY KEY ("RoomsId")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220521033026_db2', '6.0.5');

COMMIT;

START TRANSACTION;

CREATE TABLE "Patients" (
    "PatientId" integer GENERATED BY DEFAULT AS IDENTITY,
    "LastName" text NOT NULL,
    "FirstName" text NOT NULL,
    "MiddleName" text NOT NULL,
    "BirthDate" date NOT NULL,
    "GenderId" integer NOT NULL,
    "MobileNumber" text NOT NULL,
    e_mail text NOT NULL,
    inn text NOT NULL,
    id_gender integer NOT NULL,
    CONSTRAINT "PK_Patients" PRIMARY KEY ("PatientId"),
    CONSTRAINT "FK_Patients_Genders_id_gender" FOREIGN KEY (id_gender) REFERENCES "Genders" (id_gender) ON DELETE CASCADE
);

CREATE INDEX "IX_Patients_id_gender" ON "Patients" (id_gender);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220527091225_InitialCreated', '6.0.5');

COMMIT;
