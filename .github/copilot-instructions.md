### Quick orientation — what this repo is

This repository is the SSW Clean Architecture template implementation (ImposterSyndrome). It contains a minimal API Web API, an optional Nuxt WebUI, an EF Core-based Infrastructure layer and small tooling projects for migrations and local hosting.

Key folders to read first:
- `src/WebApi` — minimal API endpoints and request pipeline (`Program.cs`, `Endpoints/*`).
- `src/Application` — MediatR handlers, validators and application behaviours (`DependencyInjection.cs`).
- `src/Domain` — domain models, aggregates, domain events and eventual-consistency primitives.
- `src/Infrastructure` — EF Core `ApplicationDbContext`, interceptors, middleware (notably `EventualConsistencyMiddleware`) and persistence configuration.
- `tools/MigrationService` and `tools/AppHost` — tooling used to run migrations, seed data and host the solution locally.

Important single files:
- `Program.cs` (WebApi) — how the runtime is wired: adds ServiceDefaults, Application, Infrastructure, maps endpoints and enables `EventualConsistencyMiddleware`.
- `src/Infrastructure/Persistence/ApplicationDbContext.cs` — EF model sets and conventions (Vogen converters registered here).
- `tools/MigrationService/Initializers/ApplicationDbContextInitializer.cs` — seeding logic and use of Bogus for test data.

Project-specific patterns and conventions
- Minimal APIs: endpoints live in `src/WebApi/Endpoints/*` and are mapped from `Program.cs` using `app.MapXxxEndpoints()`.
- MediatR + Result pattern: Application layer uses MediatR and ErrorOr/Result instead of throwing for flow control; see `Application/DependencyInjection.cs` for behaviours (ValidationErrorOrResultBehavior).
- Specification pattern: business logic is written against specifications (see `Application/Common/Specifications` and tests). This avoids EF mocking in unit tests.
- Eventual consistency: domain events are queued by interceptors and processed by `EventualConsistencyMiddleware` after the response completes — be cautious when moving work into synchronous request flow. See `DispatchDomainEventsInterceptor` and `EventualConsistencyMiddleware`.
- Strongly-typed IDs and Vogen: many entities use Vogen-generated ID types; EF converters are registered in `ApplicationDbContext.ConfigureConventions()`.

Developer workflows & commands
- Run the whole solution locally (recommended): use the `tools/AppHost` run configuration. From repo root:
  - Windows PowerShell: `cd tools\AppHost; dotnet run`
  - This boots the API, migration service and other helper tooling together.
- Run only WebAPI: `dotnet run` from `src/WebApi` or use the launch profiles in that project.
- Database migrations & seeding: `tools/MigrationService` contains `ApplicationDbContextInitializer`. The AppHost project wires and runs it when starting the dev host.
- Tests: the tests use Respawn/TestContainers for integration tests. Run `dotnet test` from the solution root to run unit and integration tests.

Integration & external dependencies
- Docker is required for integration tests and local development that uses TestContainers. `README.md` documents prerequisites (Docker/Podman, Dotnet 9).
- Azure: CI/CD workflows include `azd` deployment instructions in `README.md` for full cloud provisioning.

What to watch for when changing code
- Eventual consistency: don't assume domain events are handled inline; handlers can throw `EventualConsistencyException` which is intentionally processed separately.
- Database interactions: Application code expects EF conventions (Vogen converters, interceptors). Use repository/DbContext patterns already present rather than replacing with raw SQL unless necessary.
- DI registration: `Application.DependencyInjection.AddApplication` and `Infrastructure.DependencyInjection.AddInfrastructure` are the canonical places to add services.

Examples to reference when implementing features
- Adding an endpoint: copy patterns from `src/WebApi/Endpoints/HeroEndpoints.cs` and `TeamEndpoints.cs` and map in `Program.cs`.
- Seeding data: `tools/MigrationService/Initializers/ApplicationDbContextInitializer.cs` uses Bogus and transaction-safe seeding; reuse for non-test seed scripts.
- Behaviour pipeline: see `Application/Common/Behaviours` for validation, performance and exception-to-ErrorOr wiring.

If you're unsure
- Read the top-level `README.md` and the ADRs in `docs/adr` for the architectural intent.
- Search for `EventualConsistencyMiddleware` and `DispatchDomainEventsInterceptor` before changing event or persistence behaviour.

Ask me to expand any section with command examples, file links, or small code snippets to follow the project's patterns.
