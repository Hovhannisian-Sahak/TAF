pipeline {
    agent any

    parameters {
        choice(
            name: 'BROWSER',
            choices: ['Chrome', 'Firefox', 'Edge'],
            description: 'Browser to run UI tests against when manually triggered.'
        )
    }

    triggers {
        cron('H 2 * * *')
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = '1'
        BrowserType = "${params.BROWSER}"
    }

    stages {
        stage('Restore') {
            steps {
                checkout scm
                bat 'dotnet restore'
            }
        }

        stage('API Tests') {
            steps {
                catchError(buildResult: 'UNSTABLE', stageResult: 'FAILURE') {
                    bat 'dotnet test --filter "Category=API" --results-directory TestResults\\API --logger "trx;LogFileName=api.trx"'
                }
            }
        }

        stage('UI Tests') {
            steps {
                bat 'dotnet test --filter "Category!=API" --results-directory TestResults\\UI --logger "trx;LogFileName=ui.trx"'
            }
        }
    }

    post {
        always {
            junit allowEmptyResults: true, testResults: 'TestResults/**/*.trx'
            archiveArtifacts allowEmptyArchive: true, artifacts: 'TestResults/**, logs/**'
        }
    }
}
