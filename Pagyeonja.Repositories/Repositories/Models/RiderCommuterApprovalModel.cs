using Microsoft.Identity.Client;

namespace Pagyeonja.Repositories;

public class RiderCommuterApprovalModel
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public bool? ApprovalStatus { get; set; }
    public string? ProfilePath { get; set; }
}
