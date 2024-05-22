namespace Pagyeonja.Repositories;
using Pagyeonja.Entities.Entities;
public interface IBookingRequestRepository
{
  Task<BookingRequest> AddBookingRequest(BookingRequest bookingRequest);
  Task<BookingRequest> GetBookingRequest(Guid booking);
  Task<List<BookingRequest>> GetBookingRequests();
  Task BookingRequestResponse(bool response, Guid booking);
  Task<bool> IsBookingExist(Guid booking);
}
