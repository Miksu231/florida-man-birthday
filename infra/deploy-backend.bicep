param location string = resourceGroup().location
param appServicePlanName string
param webAppName string 
param dotnetEnvironment string
param resourceGroupName string

var serviceName = 'florida-man-birthday'

var tags = {
  application: serviceName
}

resource addTagsToResourceGroup 'Microsoft.Resources/tags@2023-07-01' = {
  name:'default'
  scope: resourceGroup()
  properties: {
    tags: tags
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2023-01-01' existing = {
  name: appServicePlanName
  scope: resourceGroup(resourceGroupName)
}

resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: webAppName
  location: location
  tags: tags
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    enabled: true
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v8.0'
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: dotnetEnvironment
        }
      ]
    }
  }

  resource stack 'config@2023-01-01' = {
    name: 'metadata'
    properties: {
      CURRENT_STACK: 'dotnet'
    }
  }
}
