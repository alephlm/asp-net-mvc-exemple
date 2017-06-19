# Invoicing - parking

## Architecture, Decisions and Assumptions
- It was done using .NET Core with VSCode in a linux enviroment, so is multi-plataform rady.
- An API was built not only for data import but for entire system. So you have full acces by using REST methods for dealing with entities.
- Front-end was built as a Single Page Application Angularjs. Works sending and receiving json data.
- Database used is InMemory. When the server stops it wipe all.
- For future grow exists an entitiy 'parking' that holds name of fisical parking house.
- On starting, for test porposes 2 customers(regular and premium) and one parking are automatcally generated.

#### The System has 4 main entities.
- Customer - User of the system, car owner.
- Parking - Fisical place where customers can park cars.
- Parked - Created when customer leaves the parking. Holds time and price information. 
- Invoice - Holds a list of parked items and a total value based on customer type.

## Setup
### This setup uses .NET core command line tool. You need .NET Core intalled.
### Running application:
- first install javascript dependencies with bower:  
```sh
bower install
```
- Then restore application dependencies:
```sh 
dotnet restore
```
- Then run application:  
```sh 
dotnet run
```  
it will start at localhost:5000.

### Executing tests:
- For unit test execution you need to run xunit:
```sh 
dotnet xunit
```  