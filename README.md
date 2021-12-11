# SmartNote
A .Net Core application with Clean Architecture, CQRS, EventSourcing, Domain Driver Design

## Build Status

![build status](https://github.com/linwenda/SmartNote/actions/workflows/dotnet.yml/badge.svg)

## References

- [Domain-Driven Design Reference - Eric Evans](https://www.domainlanguage.com/ddd/reference/)
- [Implementing Domain-Driven Design - Vaughn Vernon](https://github.com/VaughnVernon/IDDD_Samples)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [kgrzybek/modular-monolith-with-ddd](https://github.com/kgrzybek/modular-monolith-with-ddd)
- [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)

## How to Run

- Download and install .NET 6.0.0 or higher SDK 
- Download and install Microsoft SQL Server
- Configure ConnectionStrings in appsettings.json
```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=localhost;Initial Catalog=SmartNote;Integrated Security=true;"
  }
}
```
## Authenticate

- [Resource Owner Password Grant Type](https://www.oauth.com/oauth2-servers/access-tokens/password-grant/)

**Example Postman for an Access Token:**
![](img/authenticate.png)

Using the HTTP request header `Authorization: Bearer <access_token>`

## Run using Docker Compose

You can run whole application using [docker compose](https://docs.docker.com/compose/) from root folder:
```shell
docker-compose up
```

It will create following services: <br/>
- MS SQL Server
- Seq Server
- Application

## License

MIT license
