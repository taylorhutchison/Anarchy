![Anarchy Logo](https://raw.githubusercontent.com/taylorhutchison/Anarchy/master/Anarchy-Large.png)
# Anarchy - ASP.NET Core Middleware to simulate errors.

## Purpose
When you are building a web application you can make assumptions about the reliability and resilience of your system. We sometimes test our applications in ideal scenarios and forget that failures can occur in components we may or may not control. Anarchy is a development tool that helps you simulate errors so you can see how your application behaves when operating conditions are less than ideal.

#### :fire: Warning :fire:
Anarchy is a tool for development and testing purposes only. It is your responsibility to make sure it is not used in production scenarios. It is literally designed to make your application fail.

### How to use
Anarchy is distributed as a NuGet package. You need to add a reference to the package in your csproj file for your web project and then perform a dotnet restore.

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