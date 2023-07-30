# ProjectIT

## Setting up application

### Pull Docker image for PostgreSQL

```$ docker pull postgres```

### Setup Docker container with PostgreSQL

```$ docker run --name ProjectIT_DB -e POSTGRES_DB=projectit -e POSTGRES_PASSWORD=postgres -p 5433:5432 -d postgres```

### Connecting to database server in PgAdmin

```
Host name/address: localhost
Port: 5433
Username: postgres
Password: postgres
```

## Using the application

### Creating and applying migrations

Make sure you have EF tools installed:

```$ dotnet tool install --global dotnet-ef```

Create new migration:

```$ dotnet ef migrations add <migration name>```

Apply migration / update database:

```$ dotnet ef database update```

### Running the application

`cd` into `ProjectIT/src/ProjectIT/Server` and run app with `dotnet run`.

## Test users

### Test Supervisor
Username: ```testsupervisor@projectititu.onmicrosoft.com```

Password: ```Sada203599```

### Test Student
Username: ```teststudent@projectititu.onmicrosoft.com```

Password: ```Roso457346```
