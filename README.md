# Florida Man Birthday

## General

Florida Man Birthday is a small full-stack application, that uses a [Google programmable search engine](https://programmablesearchengine.google.com/about/) for performing searches of for "Florida man \[date\]" to get top news articles of Florida men on any given day. The site currently offers two different functionalities: searching for today's Florida man, and choosing a date to find the Florida man for. It is possible for the service to become unavailable temporarily as Google's programmable search engine only offers 100 search queries per day for free, so the quota can run out. The daily quota is refreshed at midnight Pacific Standard Time.

## Backend

The backend has been implemented as a [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) web application running on .NET 8. The backend controls performing searches for the Google's programmable search engine and performs regular expression filtering on the results to only show desirable article-type results. The backend can be started with `dotnet run --project backend/src`. For running the backend locally, a `.env` file is required in the `backend/src` directory with your Google programmable search engine API key on the first row and the CX token on the second row.

## Frontend

The frontend is written in TypeScript using React. It consists of both custom-made components as well as imported components from [Material UI](https://mui.com/).The frontend uses [Vite](https://vitejs.dev/) with [SWC](https://swc.rs/) to build an optimize the app for deployment.

## Deployed architecture

The application is deployed on Microsoft Azure, with the backend utilizing an Azure App Service for hosting the ASP.NET Core application. The React frontend is pushed to an Azure Static Web App. The website can be found at [https://brave-meadow-0d5c67f03.5.azurestaticapps.net/](https://brave-meadow-0d5c67f03.5.azurestaticapps.net/)

## Infra-as-code

The repository contains infra-as-code for deploying the backend. The infrastructure is defined as a [Bicep](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview?tabs=bicep) file for deploying resources to Microsoft Azure. The actual deployment is performed by the CD pipeline.

## Linting

Backend linting is done using the box-standard `dotnet format` command for maintaining code quality.

Frontend uses Eslint for linting code.

## CI/CD

The CI/CD pipelines are utilizing GitHub Actions for running CI automatically on any pushes to the main branch and CD is triggered manually.

- The CI pipeline runs all tests for the repository and lints the frontend.
- The CD pipeline builds and publishes the frontend and backend separately, deploys the Azure resources with the IaC template and deploys the backend and frontend published packages to Azure App Service and Azure Static Web App respectively.

## Tests

- Backend unit tests are written using xUnit.net and can be run with a simple `dotnet test` command within the `backend` directory.
- Frontend unit tests are yet unimplemented but will be run with Jest.
- E2E tests are unimplemented as well, but will be written with Cypress.
