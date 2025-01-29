USE H2Opgave;

-- Table: Ejer
CREATE TABLE Ejer (
    EjerID INT IDENTITY(1,1) PRIMARY KEY,
    Navn NVARCHAR(255),
    Email NVARCHAR(255),
    Tlfnr NVARCHAR(20),
    Adresse NVARCHAR(255),
    KontraktStartDato DATETIME,
    KontraktSlutDato DATETIME
);

-- Table: Konsulent
CREATE TABLE Konsulent (
    KonsulentID INT NOT NULL PRIMARY KEY,
    Navn NCHAR(100),
    Email NCHAR(100),
    Tlfnr NCHAR(12),
    Adresse NCHAR(255)
);

-- Table: Område
CREATE TABLE Område (
    OmrådeID INT NOT NULL PRIMARY KEY, 
    Navn NCHAR(100), 
    KonsulentID INT NOT NULL,
    FOREIGN KEY (KonsulentID) REFERENCES Konsulent(KonsulentID)
);

-- Table: SæsonKategori
CREATE TABLE SæsonKategori (
    KategoriID INT NOT NULL PRIMARY KEY, 
    Navn NCHAR(100), 
    Uger NCHAR(50), 
    Pris INT
);

-- Table: Opsynsmand
CREATE TABLE Opsynsmand (
    OpsynsmandID INT PRIMARY KEY,
    [Rolle] NCHAR(50),
    Navn NCHAR(100),
    Phone NCHAR(12),
    Email NCHAR(100)
);

-- Table: Sommerhus
CREATE TABLE Sommerhus (
    SommerhusID INT IDENTITY(1,1) PRIMARY KEY,
    Adresse NVARCHAR(255),
    BasePris INT,
    Klassificering NVARCHAR(50),
    AntalSenge INT,
    EjerID INT NOT NULL,
    OmrådeID INT NOT NULL,
    OpsynsmandID INT NOT NULL,
    FOREIGN KEY (EjerID) REFERENCES Ejer(EjerID),
    FOREIGN KEY (OmrådeID) REFERENCES Område(OmrådeID),
    FOREIGN KEY (OpsynsmandID) REFERENCES Opsynsmand(OpsynsmandID)
);

-- Table: Kunde
CREATE TABLE Kunde (
    KundeID INT NOT NULL PRIMARY KEY,
    Email NCHAR(100),
    Tlfnr NCHAR(12),
    Adresse NCHAR(255)
);

-- Table: Reservation
CREATE TABLE Reservation (
    ReservationID INT NOT NULL PRIMARY KEY, 
    SommerhusID INT NOT NULL, 
    KundeID INT NOT NULL, 
    Pris DECIMAL(10,2), 
    StartDato DATE, 
    SlutDato DATE,
    FOREIGN KEY (SommerhusID) REFERENCES Sommerhus(SommerhusID),
    FOREIGN KEY (KundeID) REFERENCES Kunde(KundeID),
);
