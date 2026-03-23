# Restaurant Order Management System

REST API za upravljanje narudžbama restorana izgrađen s ASP.NET Core 9 i Entity Framework Core.

## Tehnologije

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

## Pokretanje projekta

### Preduvjeti

- [.NET 9 SDK](https://dotnet.microsoft.com/download)

### Koraci

1. Kloniraj repozitorij:
```bash
git clone https://github.com/Ivasecko48/AbysaltoJunior.git
cd AbysaltoJunior/junior.net/AbySalto.Junior
```

2. Pokreni migracije:
```bash
dotnet ef database update
```

3. Pokreni aplikaciju:
```bash
dotnet run
```

4. Otvori Swagger UI u browseru:
```
http://localhost:5074
```

## API Endpointi

| Metoda | Endpoint | Opis |
|--------|----------|------|
| GET | /api/restaurant/orders | Dohvati sve narudžbe |
| GET | /api/restaurant/orders/{id} | Dohvati narudžbu po ID-u |
| GET | /api/restaurant/orders/sorted | Dohvati narudžbe sortirane po iznosu |
| GET | /api/restaurant/orders/{id}/total | Dohvati ukupni iznos narudžbe |
| POST | /api/restaurant/new | Kreiraj novu narudžbu |
| PUT | /api/restaurant/orders/{id}/status | Promijeni status narudžbe |

## Status narudžbe

| Vrijednost | Opis |
|------------|------|
| 0 | Pending - Na čekanju |
| 1 | InPreparation - U pripremi |
| 2 | Completed - Završena |

## Način plaćanja

| Vrijednost | Opis |
|------------|------|
| 0 | Cash - Gotovina |
| 1 | Card - Kartica |
| 2 | R1 - R1 račun |