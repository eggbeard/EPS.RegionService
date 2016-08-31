# EPS.RegionService

Coding exercise for EPS.

## Initial thoughts on the Problem
The brief calls for storing Regions in terms of zipcode ranges, though in practice this would likely be a bad idea as it makes interrogating the data much more difficult and does not save a significant amount of storage space, but adds to the complexity of the problem.  In addition contiguous geographic regions often have no such relationship in how they are represented in zip codes. Queries such as show me the region covered by this zip, or validating that new Regions do not add zips already allocated to another region become complex and time consuming.  I have assumed that this requirement is therefore simply part of making the coding task somewhat more complex as an example.

## Data Types / Storage
US zip codes fall in the range 0 (Peuto Rico) to 99950 (Alaska) and could be represented as either char(5), String or Integer.  Using an Integer allows the slightly more efficient storage of the data, it is extensible enough to hold full 9 digit zips in the future if that is required and allows easier “between” style searches of ranges.  However were the service ever to require internationalizing then numeric storage of zip codes is no longer sufficient.  

## Architechture
In a larger application I might separate the Repository, DTO and API into individual libraries as this forces a cleaner separation of concerns.  I felt that was probably beyond the scope of the exercise here, but Namespace and Folder separation demonstrates where the split might be.

The Repository used here is extremely simple and provides only the bare minimum to show that the Region objects are reshaped correctly between data storage (implemented as a json file under App_Data here) and the Web.API. As my Repository is not asynchronous, I implemented the method of the controller using a synchronous signature rather than using async/await.
A Repository implementation might also implement a more detailed return type to acting against it.  I started on RepositoryActionStatus as an indicator of where this may go, but again felt that this was perhaps beyond the scope of the exercise.

Given my background in Geography I found it hard to ignore the problems of storing zipcode ranges, and found I had a tendency to overcomplicate the solution here - there is a lot to go wrong. 

## Testing

A simple test of the convert between DTO and “compressed” representation of a Region is included.  

Most of the testing during development was carried out in Postman

### Usage
#### Create a New Region
Issue a POST request to: /api/region
Content-Type: application/json
Body: 
```json
 {
    "name": "Example Region 2",
    "zipCodes": [
      "06108",
      "06109",
      "06110",
      "06115",
      "06033"
    ]
  }
```

Response is a 201 Created (including location header) is expected to be the same, but with the addition of an id:
```json
{
  "regionID": 1,
  "name": "Example Region 2",
  "zipCodes": [
    "06033",
    "06108",
    "06109",
    "06110",
    "06115"
  ]
}
```
Behind the scenes (App_Data/regions.json)  the shape of the data stores zipcodes as contiguous numeric ranges:
```json
  {
    "Id": 1,
    "Name": "Example Region 2",
    "ZipCodes": [
      {
        "Start": 6033,
        "End": 6033
      },
      {
        "Start": 6108,
        "End": 6110
      },
      {
        "Start": 6115,
        "End": 6115
      }
    ]
  }
```
How this would be serialized to a database would depend upon the choise of Database technology, and the implementation the job of the repository.

#### We can also Get All Regions

GET /api/region/

#### Or an Individual Region
Here the URL might come from the location header of the create POST above

e.g.
GET /api/region/1

### TODO

Next steps would probably be:
* include some proper Logging, especially of Errors.
* I would also want to add an IOC container to inject the IRepository
* The Tests need to be extended, especially for edge cases, e.g. zipcodes less than 0 or that are not numeric (i.e. input error).  A simple FakeRepository:IFakeRepository likely based in memory should be implemented to test against as the current json based implementation persists data and so tests are not reproducable.
* Implement Region Delete etc.
