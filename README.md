# MongoCsharpGenericDriver
This package contains the legacy driver and a generic data access of CRUD operations.
[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

The nuget package of the project
https://www.nuget.org/packages/MongoCsharpGenericDriver/
target framework is currently .NET Standard 2.0


## Step N°1: Create your Model based on MongoDB Schema
Example:
```sh
public class Client
{
  [BsonElement("_id")]
  public ObjectId Id { get; set; }
  [BsonElement("FirstName")]
  public string FirstName { get; set; }
}
```
Note: _id is mandatory and Id must be ObjectId !

## Step N°2: Initializing Data Access

Depends on your project type,create a local dataAccess variable type of DataAccess<T>, also make sure to initials the server and the database name in the constuctor.

Example:
```sh
private readonly DataAccess<Client> _dataAccess;

public ClientsController()
{
    string MongoClientAddress = "mongodb://localhost:27017";
    string DatabaseName = "MyMongoDB";
    _dataAccess = new DataAccess<Admin>(MongoClientAddress, DatabaseName);
}
```
### Methods and parameters:
For now we just have those method for simple CRUD operations
| Methods | details |
| ------ | ------ |
|IEnumerable<T> Get() |Get all elements from the document (example client)|
|T Get(OjectId id) |Get the element from the document by id|
|WriteConcernResult Create(T t) |Add an object in the document|
|void Update(ObjectId id, T t) |Update an object in the document|
|void Remove(ObjectId id) |Remove an object in the document|


