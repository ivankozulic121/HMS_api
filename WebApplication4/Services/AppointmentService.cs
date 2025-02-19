//using System.Data.Entity;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Services;

public class AppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentDto>> ViewAppointments( string userId)
    {
        // nadji doktora po idu u bazi
        //var doctor = await _context.Doctors.FindAsync(user.Id);
         var doctor = await _context.Doctors.FirstOrDefaultAsync( doctor => doctor.User.Id == userId);
         //var patients = await _context.Patients.Where( patient => patient.Id == doctor.Id).ToListAsync();
         var appointments = await _context.Appointments.Where( a => a.Doctor.Id == doctor.Id).Select(
             a => new AppointmentDto
             {
                 AppointmentId = a.Id,
                 PatientName = a.Patient.User.firstName + " " + a.Patient.User.lastName,
                 AppointmentTime = a.AppointmentTime,
             }).ToListAsync();
         return appointments;
    }

    public async Task BookAppointment(BookAppointmentDto bookAppointmentDto, string userId)
    {
        var doctor = await _context.Doctors.FindAsync(bookAppointmentDto.DoctorID);
        Console.WriteLine("ID:" + userId);
        var patient = await _context.Patients.FirstOrDefaultAsync( u => u.User.Id == userId);

        if (doctor == null || patient == null) throw new Exception("Invalid doctor or patient");

        var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Doctor.Id == bookAppointmentDto.DoctorID);
        //if (appointment != null) throw new Exception("Appointment already booked!");
        
        appointment = new Appointment { Doctor = doctor, Patient = patient, AppointmentTime = bookAppointmentDto.AppointmentTime };
        //Console.WriteLine($"APPOINTMENT: {appointment.Doctor.User.firstName}");
        
        _context.Appointments.Add(appointment);
        //patient.Appointments.Add(appointment);
        //doctor.Appointments.Add(appointment);
        
        await _context.SaveChangesAsync();
        //var result = await _context.SaveChangesAsync();
        
        //return appointment;
    }
}