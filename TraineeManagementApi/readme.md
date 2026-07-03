
# Trainee Management API

An ASP .NET Core Web project to understand syntax and usage of C# and .NET



## Technology Used

**Backend:** ASP.NET Core Web

**Database:** MySQL Database

**Language:** C#


## How to run

Modify the connections string in appsettings.json:

```
"ConnectionStrings": {
    "DefaultConnection": "your-connection-string"
  }
```

#### Run the following bash commands in the same directory as the .csproj file:

Apply migrations to your database:

```
dotnet ef database update
```

Run project:

```
dotnet run
```


## API Reference

#### Get all trainees

```http
  GET /api/trainee
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `none`    | `none`   | Get all the trainees       |

#### Get trainee by ID

```http
  GET /api/trainee/${id}
```

| Parameter | Type     | Description                          |
| :-------- | :------- | :----------------------------------- |
| `id`      | `long`   | **Required**. Id of trainee to fetch |



#### Get trainees by search

```http
  GET /api/trainee?search=${search}
```

| Parameter | Type     | Description                                      |
| :-------- | :------- | :----------------------------------------------- |
| `search`  | `string` | **Required**. Keyword to search the database for |



#### Post trainee

```http
  Post /api/trainee/
```

| Parameter           | Type     | Description      |
| :------------------ | :------- | :----------------------------------------- |
| `Trainee Details`   | `json`   | **Required**. Details of trainee to add to database |

#### Put Trainee

```http
  GET /api/trainee/
```

| Parameter           | Type     | Description      |
| :------------------ | :------- | :----------------------------------------- |
| `Trainee Details`   | `json`   | **Required**. Details of trainee to modify in the database |

#### Delete trainee

```http
  GET /api/trainee/${id}
```

| Parameter | Type     | Description                           |
| :-------- | :------- | :------------------------------------ |
| `id`      | `long`   | **Required**. Id of trainee to delete |



## Sample request JSON


  Post /api/trainee/
  
```
  {
    "firstName": "Ankit",
    "lastName": "Sharma",
    "email": "amit.sharma@training.com",
    "techStack": "HTML, React, JavaScript",
    "status": "Inactive"
  }
```

## Sample response JSON


  Get /api/trainee/
  
```
[
  {
    "id": 1,
    "firstName": "Ankit",
    "lastName": "Sharma",
    "email": "amit.sharma@training.com",
    "techStack": "HTML, React, JavaScript",
    "status": "Inactive",
    "createdDate": "2026-06-08T11:01:49.6515866Z",
    "updatedDate": "2026-06-08T11:01:49.6515878Z"
  }
]
```
## Known Limitations 

- No user authentication
- InMemory Database
- No frontend

## Next phase 

- Database integration
- Frontend integration

## Scope 

#### The project Trainee Management API gives a backend system to handle trainee data. It uses an internal database - InMemory database which is not persistent. THe project hass no external database or frontend integrations.
