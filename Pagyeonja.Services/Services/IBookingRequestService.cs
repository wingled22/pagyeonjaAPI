namespace Pagyeonja.Services;
using Pagyeonja.Entities.Entities;

public interface IBookingRequestService
{
  Task<BookingRequest> AddBookingRequest(BookingRequest bookingRequest);
  Task<BookingRequest> GetBookingRequest(Guid booking);
  Task<List<BookingRequest>> GetBookingRequests();
  Task BookingRequestResponse(bool response, Guid booking);
}
