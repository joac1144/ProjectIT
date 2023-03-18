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

## Running the application

`cd` into `ProjectIT/src/ProjectIT/Server` and run app with `dotnet run`.
