CREATE TABLE CouvrePlancher (
    idCouvrePlancher INT PRIMARY KEY IDENTITY(1,1),
    nomCouvrePlancher VARCHAR(255) NOT NULL,
    prixM2Materiaux DECIMAL(10, 2) NOT NULL,
    prixM2MainOeuvre DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Clients (
    idClient INT PRIMARY KEY IDENTITY(1,1),
    nomClient VARCHAR(255) NOT NULL,
    adresseClient VARCHAR(255),
);

CREATE TABLE Demandes (
    idDemande INT PRIMARY KEY IDENTITY(1,1),
    idClient INT,
    longueurPiece DECIMAL(10, 2) NOT NULL,
    largeurPiece DECIMAL(10, 2) NOT NULL,
    typeCouvrePlancher INT,
    FOREIGN KEY (idClient) REFERENCES Clients(idClient),
    FOREIGN KEY (typeCouvrePlancher) REFERENCES CouvrePlancher(idCouvrePlancher)
);

INSERT INTO CouvrePlancher (nomCouvrePlancher, prixM2Materiaux, prixM2MainOeuvre)
                       VALUES
                       ('Tapis commercial', 1.29, 2.00),
                       ('Tapis de qualité', 3.99, 2.25),
                       ('Plancher de bois franc', 3.49, 3.25),
                       ('Plancher flottant', 1.99, 2.25),
                       ('Céramique', 1.49, 3.25);