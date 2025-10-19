
\# HR Platform - .NET 8 Application



A comprehensive HR platform for managing job candidates and their skills, built with .NET 8, Blazor, and Entity Framework.



\## 🚀 Features



\- \*\*Candidate Management\*\* - Full CRUD operations for job candidates

\- \*\*Skill Management\*\* - Manage technical and soft skills

\- \*\*Advanced Search\*\* - Real-time search by name and skills

\- \*\*REST API\*\* - Complete Web API with Swagger documentation

\- \*\*Blazor UI\*\* - Modern, responsive web interface

\- \*\*Entity Framework\*\* - Code-first database approach

\- \*\*Unit Tests\*\* - Comprehensive test coverage



\## 🛠️ Technology Stack



\- \*\*Backend\*\*: .NET 8, ASP.NET Core Web API

\- \*\*Frontend\*\*: Blazor Server

\- \*\*Database\*\*: SQL Server with Entity Framework Core 8

\- \*\*Testing\*\*: xUnit, Moq

\- \*\*Architecture\*\*: Clean Architecture, Repository Pattern



\## 📋 Requirements



\- .NET 8 SDK

\- SQL Server (LocalDB included with Visual Studio)

\- Visual Studio 2022 or VS Code



\## 🏗️ Project Structure



\\`\\`\\`

Solution1/

├── HRPlatform.sln

├── src/

│   ├── HRPlatform.Domain/          # Domain entities and business rules

│   ├── HRPlatform.Application/     # Application services and DTOs

│   ├── HRPlatform.Infrastructure/  # Data access and external services

│   ├── HRPlatform.Web.API/         # REST API layer

│   └── HRPlatform.Web.Blazor/      # Blazor UI

└── tests/                          # Unit test projects

\\`\\`\\`



\## 🚀 Getting Started



\### Prerequisites

\- .NET 8 SDK

\- SQL Server (LocalDB)

\- Visual Studio 2022 or VS Code



\### Installation



1\. \*\*Clone the repository\*\*

&nbsp;  \\`\\`\\`bash

&nbsp;  git clone https://github.com/Djotao1/HRPlatform.git

&nbsp;  cd HRPlatform

&nbsp;  \\`\\`\\`



2\. \*\*Open the solution\*\*

&nbsp;  \\`\\`\\`bash

&nbsp;  # Using Visual Studio

&nbsp;  start HRPlatform.sln



&nbsp;  # Or using VS Code

&nbsp;  code .

&nbsp;  \\`\\`\\`



3\. \*\*Configure database connection\*\*

&nbsp;  - Update connection string in \\`HRPlatform.Web.API/appsettings.json\\`

&nbsp;  \\`\\`\\`json

&nbsp;  {

&nbsp;    \\"ConnectionStrings\\": {

&nbsp;      \\"DefaultConnection\\": \\"Server=(localdb)\\\\\\\\mssqllocaldb;Database=HRPlatformDb;Trusted\_Connection=true\\"

&nbsp;    }

&nbsp;  }

&nbsp;  \\`\\`\\`



4\. \*\*Run database migrations\*\*

&nbsp;  \\`\\`\\`bash

&nbsp;  dotnet ef database update --project src/HRPlatform.Infrastructure --startup-project src/HRPlatform.Web.API

&nbsp;  \\`\\`\\`



5\. \*\*Run the application\*\*

&nbsp;  \\`\\`\\`bash

&nbsp;  # Run API

&nbsp;  dotnet run --project src/HRPlatform.Web.API



&nbsp;  # Run Blazor UI (in separate terminal)

&nbsp;  dotnet run --project src/HRPlatform.Web.Blazor

&nbsp;  \\`\\`\\`



\### Using Visual Studio

1\. Open \\`HRPlatform.sln\\`

2\. Set multiple startup projects:

&nbsp;  - HRPlatform.Web.API

&nbsp;  - HRPlatform.Web.Blazor

3\. Press F5 to run



\## 🌐 Access Points



\- \*\*API Documentation\*\*: https://localhost:7001/swagger

\- \*\*Blazor UI\*\*: https://localhost:7002/candidates

\- \*\*Skills Management\*\*: https://localhost:7002/skills



\## 📚 API Endpoints



\### Candidates

\- \\`GET /api/candidates\\` - Get all candidates

\- \\`GET /api/candidates/{id}\\` - Get candidate by ID

\- \\`POST /api/candidates\\` - Create new candidate

\- \\`PUT /api/candidates/{id}\\` - Update candidate

\- \\`DELETE /api/candidates/{id}\\` - Delete candidate

\- \\`GET /api/candidates/search?name=john\&skills=csharp\\` - Search candidates



\### Skills

\- \\`GET /api/skills\\` - Get all skills

\- \\`GET /api/skills/{id}\\` - Get skill by ID

\- \\`POST /api/skills\\` - Create new skill

\- \\`PUT /api/skills/{id}\\` - Update skill

\- \\`DELETE /api/skills/{id}\\` - Delete skill



\## 🧪 Testing



Run unit tests:

\\`\\`\\`bash

dotnet test

\\`\\`\\`



\## 📊 Database



The application uses Entity Framework Code First with automatic seeding:

\- \*\*50 sample candidates\*\* with realistic profiles

\- \*\*40 different skills\*\* (programming, technologies, languages, soft skills)

\- \*\*Automatic skill assignments\*\* (3-8 skills per candidate)



\## 🎯 Key Features Demonstrated



\- \*\*Clean Architecture\*\* with proper separation of concerns

\- \*\*Repository Pattern\*\* with Unit of Work

\- \*\*DTO Pattern\*\* for API communication

\- \*\*Dependency Injection\*\* throughout the application

\- \*\*Input Validation\*\* at multiple levels

\- \*\*Error Handling\*\* and proper HTTP status codes

\- \*\*Real-time Search\*\* with debouncing

\- \*\*Responsive UI\*\* with Bootstrap



\## 🔧 Configuration



Key configuration files:

\- \\`HRPlatform.Web.API/appsettings.json\\` - API configuration

\- \\`HRPlatform.Web.Blazor/appsettings.json\\` - Blazor configuration



\## 👥 Contributing



1\. Fork the project

2\. Create your feature branch (\\`git checkout -b feature/AmazingFeature\\`)

3\. Commit your changes (\\`git commit -m 'Add some AmazingFeature'\\`)

4\. Push to the branch (\\`git push origin feature/AmazingFeature\\`)

5\. Open a Pull Request



\## 📄 License



This project is licensed under the MIT License - see the \[LICENSE](LICENSE) file for details.



\## 🤝 Support



If you have any questions or issues, please open an issue on GitHub.



\## 🙏 Acknowledgments



\- .NET Team for the excellent framework

\- Blazor team for the amazing web framework

\- Entity Framework team for the robust ORM



---



\*\*Developed with ❤️ using .NET 8, Blazor, and Entity Framework\*\*

"@ | Out-File -FilePath README.md -Encoding UTF8

