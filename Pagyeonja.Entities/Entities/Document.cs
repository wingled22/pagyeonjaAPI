using System;
using System.Collections.Generic;

namespace Pagyeonja.Entities.Entities;

public partial class Document
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? UserType { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentView { get; set; }

    public string? DocumentPath { get; set; }
}
