& .\StartDocker.ps1

Write-Host "Starting Unit Tests"
dotnet test .\Rundeck.Api.Test\Rundeck.Api.Test.csproj