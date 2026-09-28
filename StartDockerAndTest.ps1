& .\StartDocker.ps1

Write-Host "Starting Unit Tests"
dotnet test --project .\Rundeck.Api.Test\Rundeck.Api.Test.csproj