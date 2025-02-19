using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Services;

public class PatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllPatients()
    {
        //var list =  await _context.Patients.Include(patient => patient.Appointments).ToListAsync();
        //if (list == null) Console.WriteLine("LIST IS NULL");
        //Console.WriteLine("LIST: " + string.Join(" | ", list));
        //return list;
        //Console.WriteLine("HERE IS THE USER ROLE: " + user.userRole);
        /*if (user.userRole == "Doctor")
        {
            // nadji doktora po idu u bazi
            //var doctor = await _context.Doctors.FindAsync(user.Id);
            var doctor = await _context.Doctors.FirstOrDefaultAsync( doctor => doctor.User.Id == user.Id);
            Console.WriteLine("DOCTOR HEREE: " + doctor.Id);
            
            //return await _context.Patients.Where( patient => patient.Id == doctor.Id).ToListAsync();
            var patients = await _context.Patients.Where( patient => patient.Id == doctor.Id).ToListAsync();
            return patients;
            foreach (var patient in patients)
            {
                Console.WriteLine("HERE ARE PATIENTS: " + patient.Id);
            }
        }*/
        
        return await _context.Patients.ToListAsync();
        
    }

    public async Task<Patient> GetPatientById(int id)
    {

        var patient = await _context.Patients.FindAsync(id);
        Console.WriteLine("PATIENT ID: " + patient.Id);
        Console.WriteLine("PATIENT RECORDS: " + patient.Records);
        Console.WriteLine("PATIENT APPOINTMENTS:" + patient.Appointments);

        return patient;
    }

    public async Task<PatientRecord> CreateNewRecord(int id, NewRecordDto newRecordDto)
    {
        
            //var patient = await _context.Patients.Include(p => p.Records).Include( p => p.Appointments).FirstOrDefaultAsync( p => p.Id == id);
            //Console.WriteLine("PATIENTSSS ID: " + patient.Id);
            
            var patient = await _context.Patients.FindAsync(id);

            var record = _context.Records.FirstOrDefault(rec => rec.RecordName == newRecordDto.RecordName);

            var patientRecord = new PatientRecord { Record = record, RecordDate = DateTime.Now.ToUniversalTime(), Patient = patient };
            
            _context.PatientRecords.Add(patientRecord);
            await _context.SaveChangesAsync();
            
            Console.WriteLine("RECORDSSS: " + record.RecordName);
            
            //patient.Records.Add(record);
            
            //await _context.SaveChangesAsync();

            return patientRecord;






        

       
        
    }
}