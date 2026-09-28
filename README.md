[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[![Codacy Badge](https://app.codacy.com/project/badge/grade/Rundeck.Api)](https://app.codacy.com/gh/panoramicdata/Rundeck.Api/dashboard)

# Rundeck.Api

API for Rundeck

[![Nuget](https://img.shields.io/nuget/v/Rundeck.Api)](https://www.nuget.org/packages/Rundeck.Api/)

## Running the tests

Every test calls a live Rundeck, so the tests run locally, not in CI. The repository includes a disposable
Rundeck 3.2.3 in Docker whose API token is in `Dockerfiles/tokens.properties`. It is a local test server
with no production data.

1. Tell the tests where the server is. Settings come from [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)
   (`UserSecretsId` `rundeck-api-tests`), never from a file in the repository:

   ```powershell
   $p = '.\Rundeck.Api.Test\Rundeck.Api.Test.csproj'
   dotnet user-secrets set 'Rundeck:Uri' 'http://127.0.0.1:4440' --project $p
   dotnet user-secrets set 'Rundeck:Token' '<the admin token from Dockerfiles/tokens.properties>' --project $p
   dotnet user-secrets set 'Rundeck:Username' 'admin' --project $p
   ```

   Environment variables (`Rundeck__Uri`, `Rundeck__Token`, `Rundeck__Username`) work too, and override
   user secrets. `Rundeck.Api.Test/appsettings.example.json` shows the shape.

2. Start the server and run the tests: `.\StartDockerAndTest.ps1`. Stop the server with `.\StopDocker.ps1`.

Test classes share one server and one `Test` project, so they do not run in parallel
(`parallelizeTestCollections: false` in `xunit.runner.json`).
