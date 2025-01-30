USE H2Opgave;
GO

-- Ryd tabellerne (valgfrit):
-- Hvis du vil køre skriptet flere gange i træk, kan du evt. nulstille identity og slette rækker:
-- TRUNCATE TABLE Reservation;
-- TRUNCATE TABLE Kunde;
-- TRUNCATE TABLE Sommerhus;
-- TRUNCATE TABLE Opsynsmand;
-- TRUNCATE TABLE SæsonKategori;
-- TRUNCATE TABLE Område;
-- TRUNCATE TABLE Konsulent;
-- TRUNCATE TABLE Ejer;

-------------------------------------------------------------------------------
-- 1) Ejer (IDENTITY genereres)
-------------------------------------------------------------------------------
INSERT INTO Ejer (Navn, Email, Tlfnr, Adresse, KontraktStartDato, KontraktSlutDato)
VALUES
('Hans Hansen', 'hans@example.com', '12345678', 'Havrevænget 12, 6700 Esbjerg', '2020-01-01', '2025-01-01'),
('Mia Mortensen', 'mia@example.com', '87654321', 'Skovvej 8, 6000 Kolding', '2021-06-15', '2026-06-14'),
('Peter Pedersen', 'peter@example.com', '44445555', 'Strandvej 1, 9000 Aalborg', '2023-01-01', '2028-01-01');

-------------------------------------------------------------------------------
-- 2) Konsulent (IDENTITY genereres)
-------------------------------------------------------------------------------
INSERT INTO Konsulent (Navn, Email, Tlfnr, Adresse)
VALUES
(N'Lise L',    N'lise@svbo.dk', N'12345678', N'Esbjerg'),
(N'Jens J',    N'jens@svbo.dk', N'87654321', N'Kolding'),
(N'Ole O',     N'ole@svbo.dk',  N'99999999', N'Varde'),
(N'Frederik F',N'fred@svbo.dk', N'12121212', N'Ribe');

-------------------------------------------------------------------------------
-- 3) Område (IDENTITY genereres, men skal pege på KonsulentID der netop blev skabt)
--    Vi antager at ovenstående indsættelser gav:
--    1: Lise L
--    2: Jens J
--    3: Ole O
--    4: Frederik F
-------------------------------------------------------------------------------
INSERT INTO Område (Navn, KonsulentID)
VALUES
(N'Nord', 1),    -- peger på Lise L
(N'Syd', 1),
(N'Øst', 2),
(N'Vest', 3),
(N'City', 4);

-------------------------------------------------------------------------------
-- 4) SæsonKategori (IDENTITY genereres)
-------------------------------------------------------------------------------
INSERT INTO SæsonKategori (Navn, Uger, Pris)
VALUES
(N'Super',  N'28,29,30,52', 5000),
(N'Høj',    N'26,27,31,32', 4000),
(N'Mellem', N'20,21,22,23,24,25,34,35', 3000),
(N'Lav',    N'1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51',  2000);

-------------------------------------------------------------------------------
-- 5) Opsynsmand (IDENTITY genereres)
-------------------------------------------------------------------------------
INSERT INTO Opsynsmand ([Rolle], Navn, Phone, Email)
VALUES
(N'Kontrol', N'Magnus M', N'11112222', N'magnus@svbo.dk'),
(N'Check',   N'Birgit B', N'22223333', N'birgit@svbo.dk'),
(N'Kontrol', N'Søren S',  N'33334444', N'soren@svbo.dk');

-------------------------------------------------------------------------------
-- 6) Sommerhus (IDENTITY genereres)
--    FK -> EjerID, OmrådeID, OpsynsmandID
--    Vi antager at EjerID = 1 (Hans), 2 (Mia), 3 (Peter)
--    OmrådeID = 1(Nord), 2(Syd), 3(Øst), 4(Vest), 5(City)
--    OpsynsmandID = 1(Magnus), 2(Birgit), 3(Søren)
-------------------------------------------------------------------------------
INSERT INTO Sommerhus (Adresse, BasePris, Klassificering, AntalSenge, EjerID, OmrådeID, OpsynsmandID)
VALUES
(N'Krusevej 5',    3000, N'Luksus',   6, 1, 1, 1),   -- Ejer Hans, Område(Nord), Opsynsmand(Magnus)
(N'Ahornvej 10',   2500, N'Standard', 4, 1, 2, 2),   -- Ejer Hans, Område(Syd),  Opsynsmand(Birgit)
(N'Granvej 12',    2800, N'DeLuxe',   5, 2, 2, 2),   -- Ejer Mia,  Område(Syd),  Opsynsmand(Birgit)
(N'Søndervig 4',   3500, N'Luksus',   6, 2, 3, 3),   -- Ejer Mia,  Område(Øst),  Opsynsmand(Søren)
(N'Bredgade 20',   2000, N'Budget',   3, 3, 4, 1),   -- Ejer Peter,Område(Vest),Opsynsmand(Magnus)
(N'Torvegade 1',   4000, N'Super',    8, 3, 5, 3);   -- Ejer Peter,Område(City),Opsynsmand(Søren)

-------------------------------------------------------------------------------
-- 7) Kunde (IDENTITY genereres)
-------------------------------------------------------------------------------
INSERT INTO Kunde (Email, Tlfnr, Adresse)
VALUES
(N'anna@kunde.dk',   N'77778888', N'Hejrevej 10, 8000 Aarhus'),
(N'bjarne@kunde.dk', N'66667777', N'Mosevej 12, 8200 Aarhus N'),
(N'carl@kunde.dk',   N'55556666', N'Lyngbyvej 15, 2100 København'),
(N'dorte@kunde.dk',  N'44445555', N'Nygade 3, 5000 Odense C');

-------------------------------------------------------------------------------
-- 8) Reservation (IDENTITY genereres, FK -> SommerhusID, KundeID)
--    Vi antager at ovenstående SommerhusINSERT gav:
--    1 = Krusevej 5
--    2 = Ahornvej 10
--    3 = Granvej 12
--    4 = Søndervig 4
--    5 = Bredgade 20
--    6 = Torvegade 1
--    Og KundeID = 1(Anna),2(Bjarne),3(Carl),4(Dorte)
-------------------------------------------------------------------------------
INSERT INTO Reservation ( SommerhusID, KundeID, Pris, StartDato, SlutDato)
VALUES
(5, 1, 3000.00, '2023-07-01', '2023-07-08'),  -- Sommerhus 1 -> Hans, Kunde(Anna)
(3, 2, 2800.00, '2023-08-05', '2023-08-12'),  -- Sommerhus 3 -> Mia,  Kunde(Bjarne)
(5, 3, 2000.00, '2023-09-10', '2023-09-17'),  -- Sommerhus 5 -> Peter,Kunde(Carl)
(7, 4, 2500.00, '2023-10-02', '2023-10-09');  -- Sommerhus 2 -> Hans, Kunde(Dorte)

-- Slut på testdata-script
GO
