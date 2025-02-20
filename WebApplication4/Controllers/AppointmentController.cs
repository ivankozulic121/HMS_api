using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTOs;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class AppointmentController: ControllerBase
{
    private readonly  AppointmentService _appointmentService;

    public AppointmentController(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public async Task<List<AppointmentDto>> ViewAppointments()
    {
        var userIdFromToken =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _appointmentService.ViewAppointments(userIdFromToken);
    }

    public async Task<Appointment> BookAppointment(BookAppointmentDto appointmentDto)
    {
        var userIdFromToken =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _appointmentService.BookAppointment(appointmentDto, userIdFromToken);
    }



}