# TaskManager 
Task Manager is a web application that allows users to create, update, and delete tasks. The application consists of both a backend API built with .NET and a frontend client built with Angular

## API
#API Project - Use Database First Entity FreamWork

-Insert data base first 

 - create database using sql server 

- using script
   ```
   sql script -
  
   folder inside - [API/TaskManager/SQL/TaskManageSqlScript](https://github.com/Kasunjith-Bimal/TaskManager/tree/main/API/TaskManager/SQL/TaskManageSqlScript)
  ```
 - using backup
 ```
   Backup  -
 
   folder inside - [API/TaskManager/SQL/TaskManageSqlScrip](https://github.com/Kasunjith-Bimal/TaskManager/tree/main/API/TaskManager/SQL/TaskManagerDbBackup)t
 ```


in app seting changes  - API Project

```
"ConnectionStrings": {
  "DefaultConnection": "Server={{severName}};Database={{databaseName}};Trusted_Connection=True;"
},
```

## UI 
#UI Project - UIOldLazzyLoading Project this fully developed Ui project
[https://github.com/Kasunjith-Bimal/TaskManager/tree/main/UIOldLazzyLoading]
## Development Setup UIOldLazzyLoading
## RUN ONLY UIOldLazzyLoading folder UI 
### Prerequisites

- Install [Node.js] which includes [Node Package Manager][npm]

### Setting Up a Project
change enviroment.ts 
https://github.com/Kasunjith-Bimal/TaskManager/tree/main/UIOldLazzyLoading/src/environments
```
export const environment = {
    baseUrl: '{YourAPIbaseURL}/'
};
```

Install the packages:

```
npm install
```

Run the application:

```
cd [PROJECT NAME]
ng serve
```
