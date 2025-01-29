# Sydvest-Bo Administrationssystem

## Beskrivelse
Sydvest-Bo er et administrationssystem til håndtering af sommerhusudlejning. Systemet er udviklet til at hjælpe udlejningsfirmaet med at administrere sommerhuse, reservationer, kunder og personale.

## Hovedfunktioner

### 1. Sommerhusadministration
- Oprettelse, redigering og sletning af sommerhuse
- Håndtering af sommerhusets basispris
- Klassificering af sommerhuse (luksus, normal, basic)
- Tilknytning af opsynsmænd til sommerhuse

### 2. Reservationssystem
- Oprettelse af reservationer med validering af perioder
- Automatisk prisberegning baseret på sæsonkategorier
- Visning af ledige perioder
- Håndtering af overlappende reservationer

### 3. Sæsonkategorier
- Fire sæsonkategorier: super, høj, mellem og lav
- Fleksibel prissætning med multiplikatorer
- Tilknytning af specifikke uger til sæsonkategorier

### 4. Personaleadministration
- Håndtering af udlejningskonsulenter
- Administration af opsynsmænd
- Tilknytning af personale til specifikke områder

### 5. Kundeadministration
- Registrering og vedligeholdelse af kundeinformation
- Oversigt over kunders reservationer
- Håndtering af kundehistorik

## Tekniske Detaljer

### Systemkrav
- .NET 6.0 eller nyere
- Microsoft SQL Server
- Windows/macOS/Linux operativsystem

### Database
Systemet bruger en SQL Server database med følgende hovedtabeller:
- Sommerhus
- Reservation
- SæsonKategori
- Kunde
- Opsynsmand
- Konsulent
- Område

### Arkitektur
- Konsol-baseret brugergrænsflade
- Objektorienteret design med klare ansvarsområder
- Robust fejlhåndtering
- Dokumenteret kodebase med XML kommentarer

## Installation og Brug

1. Klon repository'et:
```bash
git clone [repository URL]
```

2. Opret databasen:
- Kør SQL scripts fra 'Database' mappen
- Opdater connection string i konfigurationen

3. Byg projektet:
```bash
dotnet build
```

4. Kør programmet:
```bash
dotnet run
```

## Begrænsninger
- Reservationer kan kun foretages i hele uger (lørdag til lørdag)