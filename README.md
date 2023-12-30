# LeHotelMax - Demo

This is little demo app of Clean Architecture with CQRS pattern including JWT authorization.
Task was to create JSON REST web service for hotel search. 
The service must have two API interfaces:
- CRUD interface for hotel data management (authorized)
--> Standard CRUD operations with standard validation
* Search interface that returns the list of all hotels to the user (public)
--> For each hotel, return the name, the price, and the distance from my current geo location
--> The list should be ordered. Hotels that are cheaper and closer to my current location
should be positioned closer to the top of the list. Hotels that are more expensive and
further away should be positioned closer to the bottom of the list.

### So let's start from architecture...

#### Clean Architecture
Independence of Frameworks
Separation of Concerns (structured in layers):
	* Innermost Layer (Entities/Domain Models)
	* Use Cases or Application Layer
	* Interface Adapters or Presenters
	* Frameworks and Drivers
Testability
Dependency Rule
Simplicity and Maintainability

Overall, Clean Architecture emphasizes the separation of concerns, maintainability, and testability by structuring the codebase in a way that promotes flexibility and adaptability to changes in requirements or technologies. It aims to create systems that are resilient to changes in external factors, ensuring the longevity and sustainability of software projects.

More at 
https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

So, our poject structure looks like:
```
LeHotelMax Soultion

 LeHotelMax.Application (Class library project)
	└── Common (Supporting stuff)
	└── Hotels (Use cases)
		└── CQRS (Command Query Responsibility Segregation)
	└── Users (Use cases)
  		└── CQRS (Command Query Responsibility Segregation)
	└── ServiceConfiguration.cs (Dependency injection)
	...

 LeHotelMax.Domain (Class library project)
	└── Common (Supporting stuff)
	└── Aggregates (Domain objects)
	...

 LeHotelMax.Infrastructure (Class library project)
	└── Data
		└── Identity (IdentityUser related)
		└── Repository (Persistance related)
		└── ApplicationDbContext.cs
	└── ServiceConfiguration.cs (Dependency injection)
	...

 LeHotelMax.WebApi (ASP .NET(8) Core Web API) - startup project
	└── Controllers (user interactions with service)
	└── Dtos (Data transfer objects)
	└── Logs (Logs are here)
	└── ServiceConfiguration.cs (Dependency injection)
	└── Program.cs (App entry point)
	...
```

#### CQRS pattern
CQRS is a design pattern that separates the responsibility of handling read (queries) and write (commands) operations in an application by using separate models, thereby optimizing the read and write concerns independently.

More at:
https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/apply-simplified-microservice-cqrs-ddd-patterns

### API - swagger

 
![Alt](/api_image.png "Title")

Application use InMemory Database provider but that can be easly changed to mantain persistance such as MS SQL

HOTEL API (Hotel CRUD) require User to be authorized (using JWT token)

One admin user is included:
`admin@localhost`
`Admin123!`

![Alt](/login_image.png "Title")

```	
Response body
{
  "id": "de65431b-9b6d-4c37-95dd-9c12a3a2f6d6",
  "userName": "admin@localhost",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6Ik..."
}
```

Use `accessToken` to be authorized on protected endpoints

![Alt](/auth_image.png "Title")

There is preloaded Hotels in system:
We can use pagination to fetch data

```
curl -X 'GET' \
  'https://YOUR-DOMAIN/api/v1/Hotel?PageNumber=2&PageSize=2' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIs....'
  ```

```
Response body
{
  "items": [
    {
      "id": 3,
      "name": "Sheraton Zagreb Hotel",
      "price": 163.25,
      "geoLocation": {
        "latitude": 45.8088,
        "longitude": 15.9782
      }
    },
    {
      "id": 4,
      "name": "Falkensteiner Hotel & Spa Zadar",
      "price": 200,
      "geoLocation": {
        "latitude": 44.1992,
        "longitude": 15.2103
      }
    }
  ],
  "pageNumber": 2,
  "totalPages": 4,
  "totalCount": 7,
  "hasPreviousPage": true,
  "hasNextPage": true
}
```

Rest of hotel CRUD can be tested on the same way.

#### HotelSearch API (public)

No authorization requeired here
It calucalte distance from given location (your location) to find a best match hotel for you :)

For simplicity we are using Haversine formula.
The Haversine formula is a very accurate way of computing distances between two points on the surface of a sphere using the latitude and longitude of the two points.
More at:
https://en.wikipedia.org/wiki/Haversine_formula

```
curl -X 'GET' \
  'https://YOUR-DOMAIN/api/v1/HotelSearch?Latitude=45.8088&Longitude=15.9773&PageNumber=1&PageSize=2' \
  -H 'accept: */*'
```

```
Response body
{
  "items": [
    {
      "distance": 0.06975808013819948,
      "id": 3,
      "name": "Sheraton Zagreb Hotel",
      "price": 163.25,
      "geoLocation": {
        "latitude": 45.8088,
        "longitude": 15.9782
      }
    },
    {
      "distance": 0.20736895956904278,
      "id": 1,
      "name": "Esplanade Zagreb Hotel",
      "price": 150,
      "geoLocation": {
        "latitude": 45.8105,
        "longitude": 15.9762
      }
    }
  ],
  "pageNumber": 1,
  "totalPages": 4,
  "totalCount": 7,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

` "distance": 0.06975808013819948 ` is presented in `km` from given location

### Tech stack included:
***AutoMapper*** - *library that automates the mapping of properties between object*
***MediatR*** - *implementation of the Mediator design pattern*
***FluentValidation*** - *interface for defining and applying validation rules to objects*
***Microsoft Entity Framework Core*** - *cross-platform Object-Relational Mapping (ORM) framework*
***Serilog with Sensitive Enrichers*** -  *library that provides a flexible and expressive logging API*
***Swashbuckle*** - *generate interactive API documentation*

### How to run it ?

Download solution code (or git clone)
Open it with VS2022
Set WebApi as startup project
Hit run button and wait for browser to pop-up with swagger UI

TODO:
***Tests***