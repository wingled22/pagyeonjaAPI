1. Install first the following EF packages:
	<PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.0" />
	<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.0" />
	<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.0" />
2. Run this command in the package manager console

	Scaffold-DbContext "Server=DESKTOP-E4OKK0E\SQLEXPRESS;Database=Wiggly;Trusted_Connection=True;"	Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Force

  -if docker mssql use code below
  dotnet-ef dbcontext scaffold "Server=localhost;Database=Hitch;User ID=SA;Password=VeryStr0ngP@ssw0rd;TrustServerCertificate=true;"     Microsoft.EntityFrameworkCore.SqlServer -o Entities -f

Scaffold-DbContext [-Connection] [-Provider] [-OutputDir] [-Context] [-Schemas>] [-Tables>] 
                    [-DataAnnotations] [-Force] [-Project] [-StartupProject] [<CommonParameters>]


pomelo 

replace the content of your csproj to this:
      <ItemGroup>
        <PackageReference Include="Microsoft.EntityFrameworkCore" Version="6.0.0" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="6.0.0">
          <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
          <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.3" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="6.0.0">
          <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
          <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="6.0.0" />
      </ItemGroup>
save the file then run this command to terminal dotnet ef

	dotnet ef dbcontext scaffold "server=localhost;database=test;user=root;pwd=" "Pomelo.EntityFrameworkCore.MySql" -o .\Entities -f
	dotnet ef dbcontext scaffold "server=localhost;database=olapp;user=root;pwd=" "Pomelo.EntityFrameworkCore.MySql" -o .\Entities -f


  builder.Services.AddDbContext<SampleappContext>(options =>
    options.UseMySql("server=localhost;database=sampleapp;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb")));

dotnet ef dbcontext scaffold "server=localhost;database=olapp;user=root;pwd=" "Pomelo.EntityFrameworkCore.MySql" -o Entities -f

