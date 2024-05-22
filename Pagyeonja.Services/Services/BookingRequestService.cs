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
      return await _bookingRequestRepository.AddBookingRequest(bookingRequest);
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
    }
    catch (Exception)
    {
      await _databaseTransactionRepo.RevertTransaction(transaction);
      throw;
    }
  }

  public async Task<BookingRequest> GetBookingRequest(Guid booking)
  {
    using var transaction = await _databaseTransactionRepo.StartTransaction();
    try
    {
      return await _bookingRequestRepository.GetBookingRequest(booking);
    }
    catch (Exception)
    {
      await _databaseTransactionRepo.RevertTransaction(transaction);
      throw;
    }
  }

  public async Task<List<BookingRequest>> GetBookingRequests()
  {
    using var transaction = await _databaseTransactionRepo.StartTransaction();
    try
    {
      return await _bookingRequestRepository.GetBookingRequests();
    }
    catch (Exception)
    {
      await _databaseTransactionRepo.RevertTransaction(transaction);
      throw;
    }
  }

}
