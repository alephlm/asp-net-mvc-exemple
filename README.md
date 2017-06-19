# Invoicing - parking

## Architecture, Decisions and Assumptions
- Project was done using [.NET Core](https://www.microsoft.com/net/download/core) with VSCode in a linux environment, so is multi-plataform ready.
- An API was built not only for data import but for entire system. So you have full acces by using REST methods for dealing with entities.
- Front-end was built as a Single Page Application Angularjs. Works sending and receiving json data.
- Database used is InMemory. When the server stops it wipes all.
- For future grow, exists an entitiy 'parking' that holds the name of physical parking house.
- On starting, for test porpose, 2 customers(regular and premium) and one parking are automatcally generated.

#### The System has 4 main entities.
- Customer - User of the system, car owner.
- Parking - Physical place where customers can park cars.
- Parked - Created when customer leaves the parking. Holds time and price information. 
- Invoice - Holds a list of parked items and a total value based on customer type.

## Setup
### This setup uses .NET core command line tool. You need [.NET Core](https://www.microsoft.com/net/download/core) installed.
### Running application:
- first install javascript dependencies with bower:  
```sh
bower install
```
- Then restore application dependencies:
```sh 
dotnet restore
```
- Set environment variable to development  

Windows
```sh
C:\> set ASPNETCORE_ENVIRONMENT=Development
```
Unix
```sh
$ export ASPNETCORE_ENVIRONMENT=Development
```

- Then run application:  
```sh 
dotnet run
```  
it will start at `http://localhost:5000`.

### Executing tests:
- For unit test execution you need to run xunit:
```sh 
dotnet xunit
```

## API Calls
### Customer:
* Gel All - `GET` to `localhost:5000/api/customer`
* Get by ID - `GET` to `localhost:5000/api/customer/{id}` <- customerId.
* Generate Invoice - `GET` to `localhost:5000/api/newinvoice/{id}` <- customerId.
* Insert - `POST` to `localhost:5000/api/customer` <- with json customer as data.

### Parking:
* Gel All - `GET` to `localhost:5000/api/parking`
* Get by ID - `GET` to `localhost:5000/api/parking/{id}` <- parkingId.
* Insert - `POST` to `localhost:5000/api/parking` with json parking as data.

### Parked:
* Gel All - `GET` to `localhost:5000/api/parked`
* Get by ID - `GET` to `localhost:5000/api/parked/{id}` <- parkedId.
* Insert - `POST` to `localhost:5000/api/parked` with json parked as data.

