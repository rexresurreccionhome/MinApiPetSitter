namespace PetSitter.DataStore;

using System;
using Dapper;
using Dapper.SimpleSqlBuilder;
using Dapper.SimpleSqlBuilder.FluentBuilder;

using PetSitter.Domain.Interface;
using PetSitter.Domain.Models;


public class AppointmentRepository(IDbConnectionFactory dbConnectionFactory) : IAppointmentRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    private Appointment GetPersistedAppointment(Guid appointmentId)
    {
        Appointment appointment = _dbConnectionFactory.DbConn().QuerySingle<Appointment>(
            "SELECT * FROM PetSitterSchema.Appointment WHERE AppointmentId = @AppointmentId",
            new { AppointmentId = appointmentId }
        );
        return appointment;
    }

    public Appointment CreateAppointment(AppointmentBase appointmentInput)
    {
        Appointment insertAppointment = new()
        {
            ServiceType = appointmentInput.ServiceType,
            AppointmentDateTime = appointmentInput.AppointmentDateTime,
            PetId = appointmentInput.PetId,
            SitterId = appointmentInput.SitterId,
        };
        _dbConnectionFactory.DbConn().Execute(
             @"INSERT INTO PetSitterSchema.Appointment(AppointmentId, ServiceType, AppointmentDateTime, PetId, SitterId)
            VALUES (@AppointmentId, @ServiceType, @AppointmentDateTime, @PetId, @SitterId)",
             insertAppointment
         );

        return GetPersistedAppointment(insertAppointment.AppointmentId);
    }

    public void DeleteAppointment(Guid appointmentId)
    {
        _dbConnectionFactory.DbConn().Execute(
            "DELETE FROM PetSitterSchema.Appointment WHERE AppointmentId = @AppointmentId",
            new { AppointmentId = appointmentId }
        );
    }

    public Appointment? GetAppointment(Guid appointmentId)
    {
        Appointment? appointment = _dbConnectionFactory.DbConn().QueryFirstOrDefault<Appointment>(
            "SELECT * FROM PetSitterSchema.Appointment WHERE AppointmentId = @AppointmentId",
            new { AppointmentId = appointmentId }
        );
        return appointment;
    }

    public List<Appointment> GetAppointments(ServiceTypes? serviceType = null, DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        IOrderByBuilder queryBuilder = SimpleBuilder.CreateFluent()
        .Select($"*")
        .From($"PetSitterSchema.Appointment")
        .Where(startDateTime is not null, $"AppointmentDateTime >= {startDateTime}")
        .Where(endDateTime is not null, $"AppointmentDateTime <= {endDateTime}")
        .Where(serviceType is not null, $"ServiceType = {serviceType}")
        .OrderBy($"AppointmentDateTime DESC");
        List<Appointment> appointments = [.. _dbConnectionFactory.DbConn().Query<Appointment>(queryBuilder.Sql, queryBuilder.Parameters)];
        return appointments;
    }

    public List<Appointment> GetAppointmentsByPet(Guid petId, ServiceTypes? serviceType = null)
    {
        IOrderByBuilder queryBuilder = SimpleBuilder.CreateFluent()
        .Select($"*")
        .From($"PetSitterSchema.Appointment")
        .Where($"PetId={petId}")
        .Where(serviceType is not null, $"ServiceType = {serviceType}")
        .OrderBy($"AppointmentDateTime DESC");
        List<Appointment> appointments = [.. _dbConnectionFactory.DbConn().Query<Appointment>(queryBuilder.Sql, queryBuilder.Parameters)];
        return appointments;
    }

    public List<Appointment> GetAppointmentsBySitter(Guid sitterId, ServiceTypes? serviceType = null)
    {
        IOrderByBuilder queryBuilder = SimpleBuilder.CreateFluent()
        .Select($"*")
        .From($"PetSitterSchema.Appointment")
        .Where($"SitterId={sitterId}")
        .Where(serviceType is not null, $"ServiceType = {serviceType}")
        .OrderBy($"AppointmentDateTime DESC");
        List<Appointment> appointments = [.. _dbConnectionFactory.DbConn().Query<Appointment>(queryBuilder.Sql, queryBuilder.Parameters)];
        return appointments;
    }

    public Appointment UpdateAppointment(Guid appointmentId, AppointmentBase appointmentInput)
    {
        _dbConnectionFactory.DbConn().Execute(@"UPDATE PetSitterSchema.Appointment
            SET ServiceType = @ServiceType, AppointmentDateTime = @AppointmentDateTime
            WHERE AppointmentId = @AppointmentId",
            new
            {
                AppointmentId = appointmentId,
                ServiceType = appointmentInput.ServiceType,
                AppointmentDateTime = appointmentInput.AppointmentDateTime,
            }
        );
        return GetPersistedAppointment(appointmentId);
    }
}