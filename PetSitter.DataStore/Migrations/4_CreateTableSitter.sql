CREATE TABLE PetSitterSchema.Sitter
(
    SitterId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL
);

-- DROP TABLE PetSitterSchema.Sitter;
-- EXEC sp_help 'PetSitterSchema.Sitter';