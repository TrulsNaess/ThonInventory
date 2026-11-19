# Thon Inventory

Lagerstyringssystem utviklet i samarbeid med **Thon Hotel Kristiansand** som en del av praksis i bachelorstudiet Informasjonssystemer ved Universitetet i Agder.

Målet med prosjektet har vært å lage en enklere og mer oversiktlig løsning for lagerstyring på hotellet. Systemet gir oversikt over varer, beholdning, svinn, påfyll, bestillinger og historikk, i tillegg til egne lager for resepsjon, backoffice og maintenance.

Løsningen fungerer i dag som en prototype med frontend, backend og database. Den er ikke koblet mot Thon sine eksisterende systemer.

---

## Teknologi

Prosjektet er delt opp i frontend, backend og database.

**Frontend**
- Next.js
- React
- TypeScript
- Tailwind CSS

**Backend**
- C#
- ASP.NET Core
- .NET 9
- Entity Framework Core

**Database**
- PostgreSQL
- Supabase
- Npgsql

Frontend kommuniserer med backend gjennom REST-API og JSON.

Ved lokal kjøring brukes normalt:

```text
Frontend: http://localhost:3000
Backend:  http://localhost:5003
```

---

## Hovedlager

Hovedlageret er delt inn i fire kategorier:

- BRUS
- SNACKS
- ALKOHOL
- KIOSK

For hver vare vises blant annet navn, antall, pris, lagerverdi, status og eventuell bestilling.

Det er mulig å søke etter varer og filtrere etter kategori og status. Fra løsningen kan man også registrere svinn, påfyll og bestillinger.

Produktbilder ligger i:

```text
frontend/public/products
```

---

## Dashboard

Dashboardet gir en samlet oversikt over lageret og viser blant annet:

- totalt antall produkter
- samlet beholdning
- utsolgte varer
- avvik
- aktive bestillinger
- samlet lagerverdi
- varslinger

---

## Resepsjonslager

Resepsjonslageret er laget for varer og utstyr som brukes i og rundt resepsjonen.

Eksempler er te, kaffe, hårfønere, vifter, kopper, papirvarer og skjøteledninger.

Antallet kan justeres direkte med `+` og `-`, og endringen lagres i databasen.

---

## Backoffice lager

Backoffice-lageret brukes til andre typer varer og forbruksutstyr.

Eksempler er håndklær, såpe, puter, toalettpapir, antibac og printerutstyr.

Dette lageret bruker samme database som resten av løsningen, men vises på en egen side.

---

## Maintenance lager

Maintenance-lageret er laget for teknisk utstyr og vedlikeholdsvarer.

Eksempler er batterier, fjernkontroller, hammer, skrutrekker, spikere og lyspærer.

Også her kan beholdningen justeres med `+` og `-`. Endringene lagres i databasen og beholdes etter at siden lastes inn på nytt/refresh.

---

## Database

Databasen består hovedsakelig av:

- `Categories`
- `Items`
- `SalesLogEntries`

### Categories

Kategoriene brukes til å skille de ulike lagerområdene.

```text
BRUS
SNACKS
ALKOHOL
KIOSK
RESEPSJON
BACKOFFICE
MAINTENANCE
```

### Items

En vare inneholder blant annet:

```text
Id
Name
CategoryId
DisplayOrder
Quantity
Price
IsMissing
MissingSinceDate
Notes
IsOrdered
OrderedQuantity
OrderedAtDate
ExpectedDeliveryDate
```

### SalesLogEntries

Transaksjonsloggen lagrer hendelser som skjer i hovedlageret.

Eksempler på transaksjonstyper:

```text
Sale
Restock
Shrinkage
OrderPlaced
OrderConfirmed
OrderRejected
```

---

## Påfyll og svinn

Påfyll øker lagerbeholdningen og registreres i transaksjonsloggen.

Svinn reduserer beholdningen og kan registreres med ulike årsaker:

- Gis bort
- Feilregistrert
- Borte / stjålet

Dette gjør at man senere kan se både hvor mye svinn som har vært og hvilken type svinn det gjelder.

---

## Bestillingssystem

Bestillinger registreres direkte på varen.

Det lagres blant annet:

- hvor mange som er bestilt
- når bestillingen ble registrert
- forventet leveringsdato

Når bestillingen kommer kan den bekreftes. Da legges antallet automatisk til lagerbeholdningen.

Dersom bestillingen ikke kommer eller blir kansellert, kan den avkreftes uten at lagerbeholdningen endres.

Bestillingshendelsene blir også registrert i transaksjonsloggen.

---

## Varslingssystem

Systemet varsler blant annet om lav beholdning og bestillinger som har passert forventet leveringsdato.

Grensen for lav beholdning er foreløpig satt til:

```text
30 enheter
```

Dette er en felles grense i dagens løsning. Dersom prosjektet tas videre kan det være mer hensiktsmessig å ha forskjellig minimumsbeholdning for hver vare.

---

## Transaksjonslogg

Transaksjonsloggen viser historikken i hovedlageret.

Loggen kan filtreres etter:

```text
I dag
Uke
Måned
```

og blant annet:

```text
Salg
Svinn
Påfyll
Bestillinger
Bestilling registrert
Bestilling bekreftet
Bestilling avkreftet
```

Loggen viser også:

- solgte enheter
- omsetning
- svinnverdi
- påfyllverdi

---

## Demo-kasse

Prosjektet har en Demo-Kasse som viser hvordan lageret kan fungere dersom løsningen senere kobles mot et faktisk kassasystem.

Når et salg registreres i Demo-Kassen:

1. Systemet kontrollerer at varen finnes og at beholdningen er høy nok.
2. Antallet trekkes fra lageret.
3. Salget registreres i transaksjonsloggen.
4. Lageret får oppdatert beholdning.

Demo-Kassen er kun en demonstrasjon og er ikke koblet til kassasystemet som brukes på hotellet i dag.

---

## API

Noen av de viktigste endepunktene:

### Hovedlager

```http
GET /api/inventory
```

### Transaksjonslogg

```http
GET /api/sales
```

### Salg

```http
POST /api/sales
```

### Påfyll

```http
POST /api/sales/restock
```

### Svinn

```http
POST /api/sales/shrinkage
```

### Bestilling

```http
POST /api/sales/order/register
POST /api/sales/order/confirm
POST /api/sales/order/reject
```

### Demo-kasse

```http
POST /api/sales/demo-checkout
```

### Resepsjon, Backoffice og Maintenance

```http
GET /api/location-inventory/{location}
```

Eksempel:

```http
GET /api/location-inventory/maintenance
```

For å justere antall:

```http
POST /api/location-inventory/{location}/{itemId}/adjust
```

Eksempel:

```json
{
  "delta": 1
}
```

eller:

```json
{
  "delta": -1
}
```

---

## Prosjektstruktur

Forenklet struktur:

```text
ThonInventory/
│
├── frontend/
│   ├── public/
│   │   └── products/
│   │
│   └── src/
│       ├── app/
│       │   ├── sales-log/
│       │   ├── demo-kasse/
│       │   ├── resepsjonslager/
│       │   ├── backoffice-lager/
│       │   └── maintenance-lager/
│       │
│       ├── components/
│       │   ├── InventoryDashboard.tsx
│       │   ├── InventoryList.tsx
│       │   ├── ProductDetailsPanel.tsx
│       │   ├── CategoryCard.tsx
│       │   ├── SummaryCards.tsx
│       │   └── Sidebar.tsx
│       │
│       └── lib/
│           └── api.ts
│
├── ThonInventory/
│   ├── Controllers/
│   │   ├── InventoryController.cs
│   │   ├── SalesController.cs
│   │   └── LocationInventoryController.cs
│   │
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   │
│   ├── Models/
│   ├── Migrations/
│   ├── Program.cs
│   └── ThonInventory.csproj
│
├── scripts/
└── package.json
```

---

## Kjøre prosjektet lokalt

Det må være installert:

- Git
- Node.js / npm
- .NET 9 SDK
- tilgang til Supabase-databasen

Klone prosjektet:

```bash
git clone https://github.com/TrulsNaess/ThonInventory.git
cd ThonInventory
```

Installer pakker og start prosjektet:

```powershell
npm.cmd install
npm.cmd run dev
```

Frontend blir normalt tilgjengelig på:

```text
http://localhost:3000
```

Backend:

```text
http://localhost:5003
```

---

## Databaseoppsett

Backend trenger en connection string til PostgreSQL/Supabase.

Lokal konfigurasjon ligger i:

```text
ThonInventory/appsettings.Local.json
```

Denne fila skal ikke pushes til GitHub.

`appsettings.Local.example.json` kan brukes som utgangspunkt når prosjektet settes opp på en ny maskin.

Eksempel:

```json
{
  "ConnectionStrings": {
    "SupabaseConnection": "Host=<HOST>;Port=5432;Database=postgres;Username=<USERNAME>;Password=<PASSWORD>;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

---

## Migrations

Databaseendringer håndteres gjennom Entity Framework migrations.

Fra backend-mappen:

```powershell
cd ThonInventory
```

Opprette migration:

```powershell
dotnet ef migrations add NavnPåMigration
```

Oppdatere databasen:

```powershell
dotnet ef database update
```

Migrations ligger i:

```text
ThonInventory/Migrations
```

---

## Viktig ved videre utvikling

Løsningen er foreløpig en prototype og er ikke koblet mot Thon sine faktiske systemer.

Den største videreutviklingen vil være å koble løsningen mot eksisterende kasseløsning eller andre systemer dersom det finnes tilgjengelige API-er. Da kan salg og lagerendringer registreres automatisk i stedet for manuelt.

Endringer lagres i databasen med en gang, men dersom flere personer har samme side åpen samtidig vil ikke en annen skjerm nødvendigvis oppdatere seg automatisk. Dette kan videreutvikles dersom det blir behov for full sanntidsoppdatering.

Før eventuell bruk i ordinær drift bør det også gjøres en gjennomgang av hosting, databaseoppsett, innlogging og generell sikkerhet.

---

## Sikkerhet

Databasepassord, connection strings og annen sensitiv informasjon skal ikke legges på GitHub.

Spesielt:

```text
ThonInventory/appsettings.Local.json
frontend/.env.local
```

skal kun brukes lokalt.

---

## Overlevering

Dersom prosjektet skal overtas av en ny utvikler vil vedkommende hovedsakelig trenge:

- tilgang til GitHub-prosjektet
- Node.js / npm
- .NET 9 SDK
- tilgang til Supabase
- database connection string
- lokal konfigurasjon for backend

Frontend, backend og database er separert slik at de ulike delene kan videreutvikles hver for seg.

---

## Utviklere

**Mads og Truls**, i samarbeid med **Thon Hotel Kristiansand**.

Prosjektet ble utviklet som en del av praksis i bachelorstudiet Informasjonssystemer ved Universitetet i Agder.

---

## Status

Løsningen fungerer i dag som en selvstendig prototype for lagerstyring.

Hovedfunksjonene for lager, svinn, påfyll, bestillinger, varslinger, transaksjonslogg og egne lagerområder er på plass. Dersom prosjektet skal tas videre, vil integrasjon mot Thon sine eksisterende systemer være det viktigste neste steget.
