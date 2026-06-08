# Healthcare Text Processor API

A **.NET 8 Web API** simulating a Clinical NLP pipeline that extracts and structures key clinical entities from unstructured medical text — inspired by real-world CNLP platforms.

## Tech Stack
- .NET 8 / ASP.NET Core Web API
- MSTest for unit testing
- Swagger / OpenAPI

## Features
- Extract diagnoses, medications, and symptoms from free-form clinical notes
- Structured JSON output for downstream healthcare workflows
- Keyword-based entity extraction pipeline (extendable to ML models)

## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/textprocessor/extract | Extract clinical entities from text |
| POST | /api/textprocessor/summarize | Summarize a clinical note |
| GET | /api/textprocessor/history | Get past processing results |

## Getting Started

`ash
dotnet restore
dotnet run --project HealthcareNLP.Api
`

Navigate to https://localhost:5001/swagger.