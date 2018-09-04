# Deploying to Azure
* Right-Click on the BaseProject project and select `Publish`
* Create new Resource Group, App Service Plan, ...
* Click Publish
* While that publishes, Go to portal.azure.com
* Select Resource Groups
* Select the Resource Group you created back in visual studio
* Click `+ Add`
* Search for `Sql Server`
* Select `Sql Server (logical server)` > Create
* Fill out form. On `Resource Group`, select the one you created back in Visual Studio
* Once it is provisioned, go to your new Sql Server instance then select `Properties`
* Use the info listed here, right click on the tab and select Duplicate Tab (or equivalent)
* Go to your App Service Plan (that you created back in Visual Studio in the 3rd step)
* Select `Application Settings` and Add your Production connection string and your Microsoft Key/Secret for Microsoft Auth
* Lastly, disable HTTP by going to `SSL Settings` and selecting `HTTPS Only`