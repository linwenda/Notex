# Funzone

A .Net Core application with Domain Driver Design, CQRS, Clean Architecture.

## Build Status

![build status](https://github.com/linwenda/Funzone/actions/workflows/build.yml/badge.svg)

## Database Migrations

`dotnet ef migrations add "MigrationScript" --startup-project src\Funzone.API --project src\funzone.infrastructure --output-dir DataAccess\Migrations`

## References

- [Domain-Driven Design Reference - Eric Evans](https://www.domainlanguage.com/ddd/reference/)
- [Implementing Domain-Driven Design - Vaughn Vernon](https://github.com/VaughnVernon/IDDD_Samples)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [kgrzybek/modular-monolith-with-ddd](https://github.com/kgrzybek/modular-monolith-with-ddd)
- [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)

## License

MIT license
