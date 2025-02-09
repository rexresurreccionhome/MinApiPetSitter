CREATE TABLE PetSitterSchema.Appointment
(
    AppointmentId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    ServiceType NVARCHAR(100) NOT NULL,
    AppointmentDateTime DATETIME NOT NULL,
    PetId UNIQUEIDENTIFIER NOT NULL,
    SitterId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Appointment_PetId
        FOREIGN KEY (PetId)
        REFERENCES PetSitterSchema.Pet (PetId)
        ON DELETE CASCADE,
    CONSTRAINT FK_Appointment_SitterId
        FOREIGN KEY (SitterId)
        REFERENCES PetSitterSchema.Sitter (SitterId)
        ON DELETE CASCADE
);

-- SELECT * FROM PetSitterSchema.Appointment;
-- DROP TABLE PetSitterSchema.Appointment;
-- EXEC sp_help 'PetSitterSchema.Appointment';