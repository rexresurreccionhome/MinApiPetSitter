CREATE TABLE PetSitterSchema.Pet
(
    PetId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL
);

-- DROP TABLE PetSitterSchema.Pet;
-- EXEC sp_help 'PetSitterSchema.Pet';