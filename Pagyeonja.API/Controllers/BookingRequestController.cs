using Microsoft.AspNetCore.Mvc;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services;
using Pagyeonja.API.DTO;

namespace pagyeonja.API;

[Route("api/[controller]")]
[ApiController]

public class BookingRequestController : ControllerBase
{
  private readonly IBookingRequestService _bookingRequestService;
  public BookingRequestController(IBookingRequestService bookingRequestService)
  {
    _bookingRequestService = bookingRequestService;
  }

  [HttpPost("AddBookingRequest")]
  public async Task<IActionResult> AddBookingRequest(BookingRequest bookingRequest)
  {
    try
    {
      var addedBookingRequest = await _bookingRequestService.AddBookingRequest(bookingRequest);
      return CreatedAtAction("AddBookingRequest", new { id = addedBookingRequest.BookingId }, addedBookingRequest);
    }
    catch (Exception ex)
    {
      return BadRequest($"An error occured:{ex.Message}");
    }
  }

  [HttpGet("GetBookingRequests")]
  public async Task<IActionResult> GetBookingRequests()
  {
    try
    {
      return new JsonResult(await _bookingRequestService.GetBookingRequests());
    }
    catch (Exception ex)
    {
      return BadRequest($"An error occured:{ex.Message}");
    }
  }
}
