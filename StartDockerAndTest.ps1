& .\StartDocker.ps1

Write-Output "Starting Unit Tests"
dotnet test --project .\Rundeck.Api.Test\Rundeck.Api.Test.csproj
