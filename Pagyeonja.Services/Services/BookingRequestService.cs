using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services;

public class BookingRequestService : IBookingRequestService
{
  private readonly IDatabaseTransactionRepository _databaseTransactionRepo;
  private readonly IBookingRequestRepository _bookingRequestRepository;

  public BookingRequestService(IDatabaseTransactionRepository databaseTransactionRepo, IBookingRequestRepository bookingRequestRepository)
  {
    _databaseTransactionRepo = databaseTransactionRepo;
    _bookingRequestRepository = bookingRequestRepository;
  }
  public async Task<BookingRequest> AddBookingRequest(BookingRequest bookingRequest)
  {
    using var transaction = await _databaseTransactionRepo.StartTransaction();
    try
    {
      var addedBookingRequest = await _bookingRequestRepository.AddBookingRequest(bookingRequest);
      await _databaseTransactionRepo.SaveTransaction(transaction);
      return addedBookingRequest;
    }
    catch (Exception)
    {
      await _databaseTransactionRepo.RevertTransaction(transaction);
      throw;
    }
  }

  public async Task BookingRequestResponse(bool response, Guid booking)
  {
    using var transaction = await _databaseTransactionRepo.StartTransaction();
    try
    {
      await _bookingRequestRepository.BookingRequestResponse(response, booking);
      await _databaseTransactionRepo.SaveTransaction(transaction);
    }
    catch (Exception)
    {
      await _databaseTransactionRepo.RevertTransaction(transaction);
      throw;
    }
  }

  public async Task<BookingRequest> GetBookingRequest(Guid booking)
  {
    try
    {
      return await _bookingRequestRepository.GetBookingRequest(booking);
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
      return await _bookingRequestRepository.GetBookingRequests();
    }
    catch (Exception)
    {
      throw;
    }
  }

}
