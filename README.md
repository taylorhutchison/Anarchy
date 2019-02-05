![Anarchy Logo](https://raw.githubusercontent.com/taylorhutchison/Anarchy/master/Anarchy-Large.png)
# Anarchy - ASP.NET Core Middleware to simulate errors.

## Purpose
When you are building a web application you can make assumptions about the reliability and resilience of your system. We sometimes test our applications in ideal scenarios and forget that failures can occur in components we may or may not control. Anarchy is a development tool that helps you simulate errors so you can see how your application behaves when operating conditions are less than ideal.

You configure Anarchy to match all or certain routes in your application (e.g. anything that contains /api/) and set the capture rate to decide how often you would like the matching routes to be handled by Anarchy rather than be handled by your normal application flow.

#### :fire: Warning :fire:
Anarchy is a tool for development and testing purposes only. It is your responsibility to make sure it is not used in production scenarios. It is literally designed to make your application fail.

### How to use
Anarchy is distributed as a NuGet package. You need to add a reference to the package in your csproj file for your web project and then perform a dotnet restore.

#### Installation

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <!--Add this package reference to Anarchy-->
    <PackageReference Include="Anarchy" />
  </ItemGroup>

</Project>
```

After saving your csproj file restore your packages via the command line:

```shell
dotnet restore
```

#### Configuration
Anarchy is configured in the Startup.cs file of the ASP.NET Core project. Often the Startup.cs file has lots of configuration for various parts of your application. In this example I am showing a minimal Starup.cs file.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseAnarchy(configure => {
            configure.Route("api/data", "Something went really wrong!", 418);
            configure.CaptureRate = 75;
        });
    }

    app.UseMvcWithDefaultRoute();
}
```

Notice how app.UseAnarchy is inside an if statement that checks if the app is being run in development mode. I suggest you do this to prevent you from accidently enabling Anarchy in production.

We first configure Anarchy to match any route that contains "api/data". So this will match both "https://example.com/api/data" and "https://example.com/api/database/test/", but it will not match "https://example.com/api/test/data". The other two parameters are the response text and the http status code that is returned if the route is matched and captured by Anarchy.

We set the "CaptureRate" to 75 meaning 75% of matching routes will be captured and the configured response will be sent back instead.



