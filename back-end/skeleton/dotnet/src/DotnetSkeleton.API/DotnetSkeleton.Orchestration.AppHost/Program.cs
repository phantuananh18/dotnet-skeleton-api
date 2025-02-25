var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DotnetSkeleton_API>("dotnetskeleton-api");

builder.AddProject<Projects.DotnetSkeleton_EmailModule_API>("dotnetskeleton-emailmodule-api");

builder.AddProject<Projects.DotnetSkeleton_Orchestration_APIGateway>("dotnetskeleton-orchestration-apigateway");

builder.AddProject<Projects.DotnetSkeleton_UserModule_API>("dotnetskeleton-usermodule-api");

builder.AddProject<Projects.DotnetSkeleton_IdentityModule_API>("dotnetskeleton-identitymodule-api");

builder.AddProject<Projects.DotnetSkeleton_MessageModule_API>("dotnetskeleton-messagemodule-api");

builder.Build().Run();
