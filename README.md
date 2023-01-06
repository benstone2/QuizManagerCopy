# Quiz Manager

## Software developer level 4 End point assessment

## How to run the application

* Ensure .dotnet core 5.0 is installed
* Clone git repository
* Open in visual studio
* Open the api project appsettings.json and configure your local database connection string
* Set Startup project to QuizManager.Api
* From the package manager console set the default project to QuizManager.Infrastructure and run command "update-database"
* Set multiple start up projects "QuizManager.Api" and "QuizManager.Web"

A temporary registration route has been configured which consists of three buttons to register accounts. 
Click the register link and click each button. They will disappear as the accounts are registered.

The email addresses for each account will be
*restricted@test.com
*viewer@test.com
*editor@test.com

The password for all accounts is: Password1!

