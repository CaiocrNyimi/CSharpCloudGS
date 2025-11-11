#!/bin/bash

# ============================
# CONFIGURAÇÕES FIXAS
# ============================
RG="rg-azurewebapp"
LOCATION="brazilsouth"
APP_PLAN="plan-skill4green"
RUNTIME="DOTNETCORE:9.0"

# ============================
# CONFIGURAÇÕES SENSÍVEIS
# ============================
SQL_SERVER="${SQL_SERVER:?Variável de ambiente SQL_SERVER não definida}"
SQL_ADMIN="${SQL_ADMIN:?Variável de ambiente SQL_ADMIN não definida}"
SQL_PASSWORD="${SQL_PASSWORD:?Variável de ambiente SQL_PASSWORD não definida}"
SQL_DB="${SQL_DB:?Variável de ambiente SQL_DB não definida}"
WEBAPP_NAME="${WEBAPP_NAME:?Variável de ambiente WEBAPP_NAME não definida}"

# ============================
# GRUPO DE RECURSOS
# ============================
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

# ============================
# EXECUTAR SCRIPT SQL
# ============================
sqlcmd -S "$SQL_SERVER.database.windows.net" -U "$SQL_ADMIN" -P "$SQL_PASSWORD" -d "$SQL_DB" -i "scripts/script-bd.sql"