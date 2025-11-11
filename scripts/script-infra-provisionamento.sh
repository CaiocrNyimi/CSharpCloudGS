#!/bin/bash

# ============================
# CONFIGURAÇÕES GERAIS
# ============================
RG="rg-azurewebapp"
LOCATION="brazilsouth"

# ============================
# CONFIGURAÇÕES DO BANCO SQL
# ============================
SQL_SERVER="sqlserver-rm5556331"
SQL_ADMIN="hzlnca"
SQL_PASSWORD="SenhaTop123455@"
SQL_DB="webappdb"

# ============================
# CONFIGURAÇÕES DO WEB APP
# ============================
APP_PLAN="plan-skill4green"
WEBAPP_NAME="app-skill4green"
RUNTIME="DOTNETCORE:9.0"

# ============================
# LOGIN E GRUPO DE RECURSOS
# ============================
az login
az group create --name $RG --location $LOCATION

# ============================
# BANCO DE DADOS SQL
# ============================
az sql server create \
  --name $SQL_SERVER \
  --resource-group $RG \
  --location $LOCATION \
  --admin-user $SQL_ADMIN \
  --admin-password $SQL_PASSWORD \
  --enable-public-network true

az sql db create \
  --resource-group $RG \
  --server $SQL_SERVER \
  --name $SQL_DB \
  --service-objective Basic \
  --backup-storage-redundancy Local \
  --zone-redundant false

az sql server firewall-rule create \
  --resource-group $RG \
  --server $SQL_SERVER \
  --name AllowAll \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 255.255.255.255

# ============================
# APP SERVICE PLAN + WEB APP
# ============================
az appservice plan create \
  --name $APP_PLAN \
  --resource-group $RG \
  --location $LOCATION \
  --sku F1 \
  --is-linux

az webapp create \
  --resource-group $RG \
  --plan $APP_PLAN \
  --name $WEBAPP_NAME \
  --runtime "$RUNTIME"

echo "✅ Infraestrutura provisionada com sucesso!"