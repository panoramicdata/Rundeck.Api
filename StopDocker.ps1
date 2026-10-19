docker stop rundeck-test

# The container is started with --rm; wait for its removal so an immediate restart can reuse the name
while (docker ps -aq --filter name=^rundeck-test$) {
    Start-Sleep -Seconds 1
}