using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
  public class BookingRequestRepository : IBookingRequestRepository
  {
    private readonly HitchContext _context;

    public BookingRequestRepository(HitchContext context)
    {
      _context = context;
    }
    public async Task<BookingRequest> AddBookingRequest(BookingRequest bookingRequest)
    {
      try
      {
        do
        {
          bookingRequest.BookingId = Guid.NewGuid();
        } while (await IsBookingExist(bookingRequest.BookingId));

        await _context.BookingRequests.AddAsync(bookingRequest);
        await _context.SaveChangesAsync();
        return bookingRequest;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task BookingRequestResponse(bool response, Guid booking)
    {
      try
      {
        BookingRequest _booking = await GetBookingRequest(booking);
        _booking.BookingStatus = response;
        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<BookingRequest> GetBookingRequest(Guid booking)
    {
      try
      {
        var _booking = await _context.BookingRequests.FirstOrDefaultAsync(b => b.BookingId == booking);
        return _booking;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<List<BookingRequest>> GetBookingRequests()
    {
      try
      {
        return await _context.BookingRequests.Where(b => b.BookingStatus == null).ToListAsync();
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<bool> IsBookingExist(Guid booking)
    {
      try
      {
        return await _context.BookingRequests.AnyAsync(b => b.BookingId == booking);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
