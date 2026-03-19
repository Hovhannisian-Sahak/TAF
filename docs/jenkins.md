# Jenkins CI setup

This repo uses a Jenkins declarative pipeline defined in `Jenkinsfile`.

## Requirements
- Jenkins with a Windows agent that has .NET SDK 8 installed.
- Git plugin.
- If you want PR builds: GitHub Branch Source (or equivalent for your VCS).

## Triggers
- Pull request: use a multibranch pipeline job with PR discovery enabled. Jenkins will automatically build PRs that include the `Jenkinsfile`.
- Schedule: configured in the `Jenkinsfile` (`cron('H 2 * * *')`).
- Manual start: default Jenkins behavior. The `BROWSER` parameter is shown for manual runs.

## Pipeline behavior
- API tests run first.
- UI tests run second even if API tests fail.
- Test results (`*.trx`) and any screenshots/logs are published as artifacts.

## Browser selection
When running manually, choose the `BROWSER` parameter (Chrome, Firefox, Edge). This maps to the `BrowserType` configuration via environment variables.
