# Dotnet Skeleton
A simple .NET Core project skeleton with a basic structure.

## Overview
This project is a simple .NET Core project skeleton with a basic structure and some useful tools. It is intended to be used as a starting point for new projects.

## Table of Contents
- [Overview](#overview)
- [Table of Contents](#table-of-contents)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Architecture](#architecture)
- [Contribution Guidelines](#contribution-guidelines)


## Getting Started
### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/downloads)

### Installation & Run locally
1. Clone the repository

```bash
# Go to your workspace, then open CMD and start to clone the project by this command
git clone http://11.11.7.86:7990/scm/dwf/back-end1.git
cd .\skeleton\dotnet\src
```
2. Update the secret key and value to `appsettings.json` to run locally

```bash
{
  "Logging": {
    "LogLevel": {                                # Log level configuration
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Env": "Development",                           # .NET Envỉonment
  "DatabaseOptions": {
    "MySQLConnectionString": "server=SERVER_ADDRESS;port=SERVER_PORT;database=DATABASE_NAME;uid=SERVER_USERID;password=SERVER_USER_PASSWORD;Convert Zero Datetime=true;old guids=true",                # The connection string of MySQL (omit if using MongoDB)
    "MongoDBConnectionString": "mongodb://SERVER_USERNAME:SERVER_USER_PASSWORD@SERVER_ADDRESS:SERVER_PORT/?retryWrites=true&w=majority&appName=APPLICATION_NAME"                     # The conection string of MongoDB (omit if using MySQL)
  },
  "TokenOptions": {
    "JwtSecretKey": "***",                        # Secret key is used to generate and verify JWT
    "JwtExpirationTime": "***"                    # Expiration time of JWT by minute (example: 60, 120,... )
    "RefreshTokenSecretKey": "***",               # Secret key is used to generate and verify refresh token
    "RefreshTokenExpirationTime": "***",          # Expiration time of refresh token by minute (example: 72, 512,...)
    "ForgotPasswordTokenSecretKey": "***",        # Secret key is used to generate and verify jwt that used for reset password
    "ForgotPasswordTokenExpirationTime": "***"    # Expiration time of token that used for reset password by minute (example: 60, 120,... )
  },
  "EncryptOptions": {
    "BcryptSaltRound": "***"                      # Salt round (example: 10)
  },
  "SystemInfoOptions": {
    "SystemEmail": "***",                         # The email address of system
    "ForgotPasswordCallbackUrl": "****"           # The forgot password callback URL
  },
  "MailOptions": {
    "NoReply": {
      "Mail": "***",                              # The noreply email address
      "Password": "***",                          # The password of noreply email
      "Host": "***",                              # The host of noreply email
      "Port": 587                                 # The port of noreply email
    }
  }
}

```
3. Run the source code

```bash
dotnet restore
dotnet build
dotnet run --project ./DotnetSkeleton.API/DotnetSkeleton.API.csproj
```

4. Testing

```bash
dotnet test
```

5. Deployment
```bash
# TO BE UPDATED
```

## Architecture
1. Technology Stack
    - Clean Architecture
    - .NET 8.0
    - Entity Framework Core
    - Database:
    - MySQL
    - MongoDB
    - Authentication: JWT

2. Code Directory Structure
```
.
├── DotnetSkeleton
│   ├── Controllers
│   │   ├── PingController.cs
│   │   └── ...
│   ├── Utils                               # Utilities
│   │   └── ControllerExtensions.cs
│   ├── Dockerfile                          # Docker image
│   ├── DotnetSkeleton.csproj
│   ├── DotnetSkeleton.sln
│   ├── Program.cs                          # Main entry (Setup, config, server, etc)
│   └── README.md
├── DotnetSkeleton.Application
│   ├── Mappings                            # AutoMapper profile
│   │   └── MappingUser.cs
│   │   └── ...
│   ├── Services                            # Handle logic
│   │   └── UserService.cs
│   │   └── ...
│   ├── DependencyInjection.cs
│   └── DotnetSkeleton.Application.csproj
├── DotnetSkeleton.Domain
│   ├── Entities                            # Database entity
│   │   ├── BaseEntity.cs
│   │   └── ...
│   ├── Interfaces
│   │   ├── Repositories
│   │   │   ├── IBaseRepository.cs
│   │   │   └── ...
│   │   └── Services
│   │       └── IUserService.cs
│   ├── Models
│   │   ├── Options                         # Store credential and config
│   │   │   └── DatabaseOptions.cs
│   │   ├── Responses
│   │   │   └── BaseResponse.cs
│   │   └── Requests
│   │       └── BaseRequest.cs
│   ├── Resources                           # Multiple language
│   │   ├── Resources.Designer.cs
│   │   ├── Resources.en-US.resx
│   │   └── Resources.resx
│   └── DotnetSkeleton.Domain.csproj
├── DotnetSkeleton.Infrastructure
│   ├── DbContexts                          # Database context
│   │   └── SkeletonDbContext.cs
│   ├── Repositories                        # Communicate with database
│   │   ├── BaseRepository.cs
│   │   └── ...
│   ├── DependencyInjection.cs
│   └── DotnetSkeleton.Infrastructure.csproj
└── DotnetSkeleton.Test                     # Unit test
    ├── DotnetSkeleton.Test.csproj
    └── UnitTest1.cs
```

## Contribution Guidelines
- Before committing your changes, you must build the source and run the Unit tests to ensure your new change will pass those checking.
- You are not allowed to commit directly to `master` or `develop` branch, you will need to create new branch with a meaninful name (i.e. using the JIRA ticket created for the change, for an example `DWF-100`)
- Make your changes, and commit the change with meaningful commit message (i.e. using original JIRA ticket KEY: TITLE i.e. `DWF-100: Change database to add new user table` as the commit message, you can add more sub items need - using multiple lines)
- Create new Pull Request to review and merge to `develop` branch. The title of PR will be the same as the first line of commit message (i.e. `DWF-100: Change database to add new user table`).