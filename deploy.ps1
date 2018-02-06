# Create a resource group.
az group create --location northeurope --name mksm-funcs

az group deployment create `
    --resource-group mksm-funcs `
    --template-file azuredeploy.json `
    --parameters azuredeploy.parameters.json

# Create an App Service plan in `FREE` tier.
#az appservice plan create --name $webappname --resource-group myResourceGroup --sku FREE

# Create a web app.
#az webapp create --name $webappname --resource-group myResourceGroup --plan $webappname

# Configure continuous deployment from GitHub. 
# --git-token parameter is required only once per Azure account (Azure remembers token).
#az webapp deployment source config --name $webappname --resource-group myResourceGroup \
#--repo-url $gitrepo --branch master --git-token $token

# Copy the result of the following command into a browser to see the web app.
#echo http://$webappname.azurewebsites.net