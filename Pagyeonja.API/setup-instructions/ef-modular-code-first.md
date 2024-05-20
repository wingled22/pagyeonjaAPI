# Code-first approach but models are modular

To apply code-first Entity Framework Core (EF Core) to your class library project containing the `DbContext` and entity classes, you can follow these steps. This will ensure your project is set up for migrations and database updates.

### Step-by-Step Workflow

#### Step 1: Add EF Core Tools

Ensure your class library project has the necessary EF Core packages. In your terminal, navigate to your project directory and run:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

#### Step 2: Update Your DbContext

Ensure your `HitchContext` class is correctly configured for dependency injection and that it does not hardcode the connection string:

```csharp
using Microsoft.EntityFrameworkCore;

namespace Pagyeonja.Entities.Entities
{
    public partial class HitchContext : DbContext
    {
        public HitchContext()
        {
        }

        public HitchContext(DbContextOptions<HitchContext> options)
            : base(options)
        {
        }

        // DbSet properties ...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity configurations...

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
```

#### Step 3: Configure the Startup Project

Assume you have a separate startup project (e.g., a console app or web API) that references this class library. In your startup project, add the necessary EF Core packages if not already done:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

#### Step 4: Configure Services in Startup Project

In your startup project, configure the `DbContext` in `Program.cs` or `Startup.cs`:

**Program.cs for .NET 6+ minimal API:**

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HitchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services...

var app = builder.Build();

// Configure middleware...

app.Run();
```

**appsettings.json:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Hitch;User ID=SA;Password=VeryStr0ngP@ssw0rd;TrustServerCertificate=true;"
  }
}
```

#### Step 5: Add Migrations

Make sure your startup project is set as the startup project in your solution. Then, open a terminal and navigate to your startup project directory. Use the EF Core CLI tools to add and apply migrations:

```bash
dotnet ef migrations add InitialCreate --project ../Pagyeonja.Entities --startup-project .
dotnet ef database update --project ../Pagyeonja.Entities --startup-project .
```

Here, `--project` specifies the class library containing `HitchContext`, and `--startup-project` specifies the project that will execute the migrations.

#### Step 6: Verify and Use

Ensure your application is configured correctly, and you can now run your application. EF Core will manage the database schema based on your entity configurations.

### Example Project Structure

```
Solution
│
├── Pagyeonja.Entities
│   ├── Entities
│   │   ├── Approval.cs
│   │   ├── Commuter.cs
│   │   ├── Document.cs
│   │   ├── Review.cs
│   │   ├── RideHistory.cs
│   │   ├── Rider.cs
│   │   ├── Suspension.cs
│   │   ├── TopupHistory.cs
│   │   └── Transaction.cs
│   ├── HitchContext.cs
│   └── Pagyeonja.Entities.csproj
│
└── StartupProject
    ├── appsettings.json
    ├── Program.cs
    └── StartupProject.csproj
```

### Summary

1. **Add necessary EF Core packages to both projects.**
2. **Ensure `DbContext` configuration is flexible and does not hardcode the connection string.**
3. **Configure `DbContext` in the startup project.**
4. **Use EF Core CLI tools to manage migrations and database updates.**

By following these steps, you will have a working setup for applying code-first EF Core migrations to your class library project.