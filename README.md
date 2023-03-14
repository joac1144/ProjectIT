# ProjectIT

## Running the application

### Setup Docker container with PostgreSQL:

```$ docker run --name ProjectIT_DB -e POSTGRES_DB=projectit -e POSTGRES_PASSWORD=postgres -p 5433:5432 -d postgres```

### Connect to database server in PgAdmin:

```
Host name/address: localhost
Port: 5433
Username: postgres
Password: postgres
```