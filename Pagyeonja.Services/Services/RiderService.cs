using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;
using PagyeonjaServices.Services;

namespace Pagyeonja.Services.Services
{
	public class RiderService : IRiderService
	{
		private readonly IRiderRepository _riderRepository;
		private readonly IDatabaseTransactionRepository _databaseTransactionRepo;
		private readonly IApprovalRepository _approvalRepository;

		public RiderService(IRiderRepository riderRepository, IDatabaseTransactionRepository databaseTransactionRepository, IApprovalRepository approvalRepository)
		{
			_riderRepository = riderRepository;
			_databaseTransactionRepo = databaseTransactionRepository;
			_approvalRepository = approvalRepository;
		}

		public async Task<IEnumerable<Rider>> GetRiders()
		{
			return await _riderRepository.GetRiders();
		}

		public async Task<IEnumerable<Rider>> GetRidersApproved()
		{
			return await _riderRepository.GetRidersApproved();
		}

		public async Task<Rider> GetRider(Guid id)
		{
			return await _riderRepository.GetRider(id);
		}

		public async Task<Rider> UpdateRider(Rider rider)
		{
			return await _riderRepository.UpdateRider(rider);
		}

		public async Task<Rider> AddRider(Rider rider)
		{
			using var transaction = await _databaseTransactionRepo.StartTransaction();
			try
			{

				await _riderRepository.AddRider(rider);

				// Create rider approval
				var approval = new Approval()
				{
					UserId = rider.RiderId,
					UserType = "Rider",
					ApprovalDate = null,
				};

				await _approvalRepository.AddApproval(approval);


				//commit changes if done
				await _databaseTransactionRepo.SaveTransaction(transaction);

				return rider;
			}
			catch (Exception ex)
			{
				await _databaseTransactionRepo.RevertTransaction(transaction);
				throw;
			}
			// return await _riderRepository.RegisterRider(rider);
		}
		public async Task<bool> DeleteRider(Guid id)
		{
			return await _riderRepository.DeleteRider(id);
		}

		public async Task<bool> RiderExists(Guid id)
		{
			return await _riderRepository.RiderExists(id);
		}

		public async Task SaveImage(Guid id, List<IFormFile> images, string doctype, string usertype, string docview)
		{
			try
			{
				var filePaths = new List<string>();
				foreach (var image in images)
				{
					// Generate a unique filename
					var extension = Path.GetExtension(image.FileName);
					var uniqueFileName = $"{Guid.NewGuid()}{extension}";

					while (await ImageExist(uniqueFileName, usertype, doctype))
					{
						uniqueFileName = $"{Guid.NewGuid()}{extension}";
					}

					// Save the image to the Images folder
					var path = Path.Combine(
						Directory.GetCurrentDirectory(),
						"wwwroot\\img",
						usertype.ToLower() == "rider" &&
						doctype.ToLower() == "profile" ? "rider_profile" :
						usertype.ToLower() == "commuter" &&
						doctype.ToLower() == "profile" ?
						"commuter_profile" : "documents", uniqueFileName);
					using var stream = new FileStream(path, FileMode.Create);
					await image.CopyToAsync(stream);
					filePaths.Add(uniqueFileName);
				}
				if (filePaths.ToArray().Length > 0)
				{
					await SaveImagePath(id, doctype, usertype, filePaths[0].ToString(), docview);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		public async Task<bool> ImageExist(string filename, string usertype, string doctype)
		{
			return await _riderRepository.ImageExist(filename, usertype, doctype);
		}

		public async Task SaveImagePath(Guid id, string doctype, string usertype, string filename, string docview)
		{
			await _riderRepository.SaveImagePath(id, doctype, usertype, filename, docview);
		}
	}
}