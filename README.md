# CSharp.MicroLiteRetryPolicy

Politique de retry pour l'ORM MicroLite : gestion des erreurs transitoires SQL Server avec stratégie de retry configurable et détection intelligente des erreurs récupérables.

## Structure

- `RetryPolicies/` — Bibliothèque principale
  - `Policies/MicroLiteRetryPolicy.cs` — Politique de retry pour MicroLite
  - `Policies/CustomRetryPolicy.cs` — Retry générique configurable
  - `Policies/CustomDatabaseTransientErrorDetectionStrategy.cs` — Détection d'erreurs transitoires DB
  - `Exceptions/SqlExceptionHelper.cs` — Analyse des codes d'erreur SQL Server
- `Business/` — Couche d'accès aux données avec Repository pattern
  - `Data/Repository.cs` — Repository avec retry intégré
  - `Data/Session.cs` — Gestion de session MicroLite
- `Contracts/` — Interface IRepository
- `UnitTestProject/` — Tests unitaires (retry, division par zéro, MicroLite)
- `MicroLitePoc/` — Console de démonstration

## Stack

C# / .NET Framework / MicroLite ORM / SQL Server
