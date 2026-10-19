# Start Rundeck on Port 4440 and map the Dockerfiles directory and configure the tokens.properties file location in that folder
# Container will automatically be removed when stopped
# The metrics settings turn on the /metrics API endpoints, which Rundeck 5 and later disable by default.
# The -D argument is quoted because PowerShell otherwise splits it at the first dot.

docker run -dt --rm -p 4440:4440 -v ${PWD}/Dockerfiles:/home/rundeck/dockerfiles -e RUNDECK_TOKENS_FILE=/home/rundeck/dockerfiles/tokens.properties -e RUNDECK_METRICS_ENABLED=true --name rundeck-test rundeck/rundeck:6.2.1 '-Drundeck.metrics.legacy.enabled=true'

# Wait for an HTTP 200 from Rundeck
do {
    try {
        $request = Invoke-webrequest http://127.0.0.1:4440
    } catch {}
    if ($request.StatusCode -eq 200) {
        Write-Host "Rundeck ready"
    }
    else {
        Write-Host "Rundeck not ready, waiting 1 second"
        sleep 1
    }
} until ($request.StatusCode -eq 200)