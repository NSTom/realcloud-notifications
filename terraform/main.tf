# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 2.26"
    }
  }
}

provider "azurerm" {
  features {}
  subscription_id = "075254db-75bd-4d17-b2a8-9fa8cdff9f65"
}

resource "azurerm_resource_group" "realcloud" {
  name = "rg_realcloud_notification_tf"
  location = "westeurope"
}

resource "azurerm_storage_account" "realcloud" {
  name = "rcnstorage"
  resource_group_name = azurerm_resource_group.realcloud.name
  location = azurerm_resource_group.realcloud.location
  account_tier = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "realcloud" {
  name = "appserviceplan_realcloud_notification"
  location = azurerm_resource_group.realcloud.location
  resource_group_name = azurerm_resource_group.realcloud.name

  sku {
    tier = "Standard"
    size = "B1"
  }
}

resource "azurerm_function_app" "realcloud" {
  name = "fn-realcloud-notification-tf"
  location = azurerm_resource_group.realcloud.location
  
  resource_group_name = azurerm_resource_group.realcloud.name
  app_service_plan_id = azurerm_app_service_plan.realcloud.id
  storage_account_name = azurerm_storage_account.realcloud.name
  storage_account_access_key = azurerm_storage_account.realcloud.primary_access_key

  https_only = true

  source_control {
    repo_url = "git@github.com:NSTom/realcloud-notifications.git"
    branch = "main"
  }
}