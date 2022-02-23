# cabzcommerce api

## Setup
---

Pre-req

![MongoDB](https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white)
![.net](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

1. You will need a MongoDB instance 
2. You will need a host to run .net 6 or later


## Configure DBSettings and ApiSettings
---

### **DBSettings**

Open `appsettings.json` inside `src/cabzcommerce.api/`

In the `DBSettings` settings you will see something like below. This section is use to define connection properties to your MongoDB database. It is directly used with the `DBSettings` class under `src/cabzcommerce.api/Helpers/DBSettings.cs`. It is instantiated in `src/cabzcommerce.api/Program.cs` line:23

```json
"DBSettings" : {
    // HOST your host with the mongo db instance in this case it is in mongodb atlas
    "Host" : "cluster0.2t0by.mongodb.net", 
    // PORT the port of your hosting mongoDB in this case the default from mongodb atlast 
    "Port" : "27017",
    // USERNAME the username of your hosting mongoDB
    "User" : "mongoadmin",
    // DBNAME your database name
    "DbName":"cabzcommercedb"
  }
```
You might notice from the `DBSettings` class that there is another property called `Password`. For security purposes it is not part of the `appsettings,json`.

To setup your database password to be use by the api. You need to set it as an environmental variable in your local machine or set it as a user secret. 

## Set password as user secret
To set the database password as a user secret use the following commands in your terminal.

```markdown
NOTE: If you clone this project, you might need to delete the current UserSecretsId inside cabzcommerce.api.csproj under <PropertyGroup> then <UserSecretsId> before you run the following commands below
``` 

Initialize dotnet Secret Manager
```bash
dotnet user-secrets init
```

Set dotnet Secret password 

`NOTE: DO NOT USE '@' as part of any value for MongoDB connection string. It is a reserved character`
```bash
dotnet user-secrets set DBSettings:Password <value>
```

### **ApiSettings**

Open `appsettings.json` inside `src/cabzcommerce.api/`

In the `ApiSettings` settings you will see something like below. This section is use to define the _Token Expiration_ and _Refresh Token expiration_ `ApiSettings` class under `src/cabzcommerce.api/Helpers/ApiSettings.cs`. It is instantiated in `src/cabzcommerce.api/Program.cs` line:24

```json
  "ApiSettings": {
    "TokenExp":1800,
    "RefreshTokenExp":3600
  }
```

### **Swagger**

OpenAPI standard is setup with the api. After setup run the app and open `https://<url>:<port>/swagger` to see the Swagger Api documentation