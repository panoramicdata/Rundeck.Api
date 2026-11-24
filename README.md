[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/9b9e434879364423a0cf12cebd7efb71)](https://app.codacy.com/gh/panoramicdata/Rundeck.Api/dashboard)

# Rundeck.Api

> **Need help with APIs or .NET?** Panoramic Data are experts in APIs and .NET, and we can help with your development. [Contact us](https://www.panoramicdata.com/contact-us) or see our [software services](https://www.panoramicdata.com/services/software).

API for Rundeck

Supports .NET 11.0.

[![Nuget](https://img.shields.io/nuget/v/Rundeck.Api)](https://www.nuget.org/packages/Rundeck.Api/)

## Breaking changes in the next release

JSON handling moved from Newtonsoft.Json to System.Text.Json. The public types and members are unchanged, but the following are visible to consumers:

- Untyped `object` values (for example `Execution.Argstring`) and `AdditionalMetrics` entries that hold a JSON object or array are now `Dictionary<string, object?>` or `List<object?>`, not `JObject` or `JArray`. Code that casts to Newtonsoft types will fail at runtime. Scalars are still `long`, `double`, `bool`, `string` and `DateTime`.
- `[DataContract]` and `[DataMember]` were removed from the model types and replaced by `[JsonPropertyName]`. Serializing these models with your own Newtonsoft.Json or `DataContractSerializer` no longer produces the Rundeck property names.
- A response that cannot be deserialized now has an inner `System.Text.Json.JsonException` (inside Refit's `ApiException`) instead of `Newtonsoft.Json.JsonReaderException`.
- `Newtonsoft.Json` and `Refit.Newtonsoft.Json` are no longer dependencies of this package.
- Reading stays lenient (case-insensitive names, nulls leave defaults, numbers and strings interchangeable, `+0000` date offsets, enums by value, name or number), but the error messages for malformed JSON differ.

## Running the tests

Every test calls a live Rundeck, so the tests run locally, not in CI. The repository includes a disposable
Rundeck 6.2.1 in Docker whose API token is in `Dockerfiles/tokens.properties`. It is a local test server
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
