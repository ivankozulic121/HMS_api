using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Services;
using WebApplication4.DTOs;

namespace WebApplication4.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PatientController: ControllerBase
{
    private readonly PatientService _patientService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatientController( PatientService patientService, UserManager<ApplicationUser> userManager)
    {
        _patientService = patientService;
        _userManager = userManager;
    }


    [HttpGet]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetAllPatients()
    {
        var userIdFromToken =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        Console.WriteLine("TOKENNNN:" + userIdFromToken);
        
        var user = await _userManager.FindByIdAsync(userIdFromToken);
        
        

        //if (user.userRole == "Doctor") 
        //{
            
        //}
        
        //Console.WriteLine("GET ALL PATIENTS CONTROLLER: " + user.UserName);
        
        
        
        
        var patients  = await _patientService.GetAllPatients();

        Console.WriteLine($"BEST DOCTOR PATIENTS: {patients}");
        
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            MaxDepth = 64
        };

        // You can use JsonResult to return the object with customized options
        return new JsonResult(patients, options);
    }

    [HttpGet("{id}")]

    public async Task<Patient> GetPatientById(int id) => await _patientService.GetPatientById(id);


    [HttpPatch("{id}")]
    [Authorize(Roles = "Doctor")]

    public async Task<ActionResult<PatientRecord>> CreateNewRecord(int id, NewRecordDto newRecordDto)
    {
        var user = await _userManager.GetUserAsync(User);
        Console.WriteLine($"PRINTING USER: {user.UserName}");
        var patientRecord  =  await _patientService.CreateNewRecord(id, newRecordDto);
        return Ok(new { PatientID = patientRecord.Patient.Id, RecordName = patientRecord.Record.RecordName });
        
    }
}