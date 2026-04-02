# AssetManagement
Managing corporate hardware (Laptops, Monitors, etc.) and their associated software licenses

## Overview
This project provides a stateless ASP.NET Core API for managing corporate hardware assets, their warranty cards, assigned employees, software licenses, and asset statuses.

### Relationship Logic
- **Asset ? WarrantyCard**: One-to-one. Every asset must have exactly one warranty card.
- **Employee ? Assets**: One-to-many. One employee can be assigned multiple assets.
- **Asset ? SoftwareLicense**: Many-to-many via a join table.
- **Status ? Assets**: One-to-many. Each asset belongs to a status category (Available, Assigned, Retired).

## Run with Docker Compose
1. Set a SQL Server password in your shell:
   - PowerShell: `$env:SA_PASSWORD = "Your_password123"`
   - Bash: `export SA_PASSWORD=Your_password123`
2. Start the environment:
   - `docker compose up --build`
3. The API will be available at `http://localhost:8080`.

## Apply EF Core Migrations
Run these commands from the repository root:
1. `dotnet tool install --global dotnet-ef` (once per machine)
2. `dotnet ef migrations add <MigrationName> --project AssetManagement --startup-project AssetManagement`
3. `dotnet ef database update --project <MigrationName> --startup-project <MigrationName>`

## Authorization
All endpoints require a JWT bearer token. There is no login endpoint yet, so generate a token manually:

1. Go to `https://jwt.io`.
2. Use header:
   - `{ "alg": "HS256", "typ": "JWT" }`
3. Use payload:
   - `{ "sub": "test-user", "iss": "AssetManagementApi", "aud": "AssetManagementApi", "role": "Admin", "exp": 4102444800 }`
4. Use the secret from `Jwt:Key` in `appsettings.json`.
5. Send requests with `Authorization: Bearer <token>`.

## API Endpoints
The API exposes CRUD endpoints for assets, employees, software licenses, and statuses. Assets include warranty card data and assigned licenses in the responses.

## Future Implementation
- Add Logging and monitoring with Serilog and Application Insights.
- Add an authentication endpoint to issue JWTs.
- Rotate keys and move secrets to a managed vault (e.g., Azure Key Vault).
- Add refresh tokens and role management. 
- Implement pagination and filtering for list endpoints.
- Implement caching for frequently accessed data (e.g., asset statuses).
   
