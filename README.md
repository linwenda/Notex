![build status](https://github.com/linwenda/Notex/actions/workflows/dotnet.yml/badge.svg)

# Notex
A Note/Blog application based on .NET 6 with DDD, CQRS and Event Sourcing

## References

- [Domain-Driven Design Reference - Eric Evans](https://www.domainlanguage.com/ddd/reference/)
- [Implementing Domain-Driven Design - Vaughn Vernon](https://github.com/VaughnVernon/IDDD_Samples)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [kgrzybek/modular-monolith-with-ddd](https://github.com/kgrzybek/modular-monolith-with-ddd)
- [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)

## How to Run

- Download and install .NET 6.0.0 or higher SDK 
- Download and install MySQL 5.7
- Configure ConnectionStrings in appsettings.json
```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Port=3306;Database=notex;Uid=root;Pwd=123456;pooling=true;CharSet=utf8;"
  }
}
```

## License

MIT license