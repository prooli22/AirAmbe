DROP TABLE IF EXISTS Utilisateurs; 
DROP TABLE IF EXISTS Types; 

DROP TABLE IF EXISTS VolScenarios; 
DROP TABLE IF EXISTS Scenarios;
DROP TABLE IF EXISTS Historiques;
DROP TABLE IF EXISTS Statuts;
DROP TABLE IF EXISTS Vols; 
DROP TABLE IF EXISTS Avions; 
DROP TABLE IF EXISTS Aeroports;


CREATE TABLE Types (
	idType	        INT				NOT NULL AUTO_INCREMENT,
	nom				VARCHAR(20)		NOT NULL UNIQUE,
	
	PRIMARY KEY (idType)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE Utilisateurs (
	idUtilisateur	INT				NOT NULL AUTO_INCREMENT,
	idType			INT				NOT NULL,
	nomUtilisateur	VARCHAR(30)		NOT NULL UNIQUE,
    motPasse		VARCHAR(32)		NOT NULL,
	prenom			VARCHAR(20),
	nom				VARCHAR(20),
	poste			VARCHAR(4),
	dateEmbauche	DATE,
	numeroTelephone VARCHAR(10),
	courriel		VARCHAR(50),
	adresse			VARCHAR(50),
	photo			BLOB,
	
	PRIMARY KEY (idUtilisateur),
	FOREIGN KEY (idType)
		REFERENCES Types(idType)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE Avions (
	idAvion	        INT				NOT NULL AUTO_INCREMENT,
	marque			VARCHAR(30)		NOT NULL,
	modele			VARCHAR(30)		NOT NULL,
	
	PRIMARY KEY (idAvion)
) CHARACTER SET utf8 COLLATE utf8_general_ci;

ALTER TABLE Avions
	ADD CONSTRAINT Avions_Marque_Modele_UK
	UNIQUE KEY (marque, modele);


CREATE TABLE Aeroports (
	idAeroport	    INT				NOT NULL AUTO_INCREMENT,
	ville			VARCHAR(30)		NOT NULL,
	codeAITA		VARCHAR(3)		NOT NULL UNIQUE,
	
	PRIMARY KEY (idAeroport)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE Vols (
	idVol			INT				NOT NULL AUTO_INCREMENT,
	idAvion			INT				NOT NULL,
	idAeroport		INT				NOT NULL,
	numeroVol		VARCHAR(10)		NOT NULL UNIQUE,
	estAtterrissage BOOL			NOT NULL,
	
	PRIMARY KEY (idVol),
	FOREIGN KEY (idAvion)
		REFERENCES Avions(idAvion),
	FOREIGN KEY (idAeroport)
		REFERENCES Aeroports(idAeroport)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE Statuts (
	idStatut    	INT				NOT NULL AUTO_INCREMENT,
	nom				VARCHAR(30)		NOT NULL,
	
	PRIMARY KEY (idStatut)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE Historiques (
	idHistorique	INT				NOT NULL AUTO_INCREMENT,
	idVol			INT				NOT NULL,
	idStatut		INT				NOT NULL,
	dateHeure		DATETIME		NOT NULL,
	
	PRIMARY KEY (idHistorique),
	FOREIGN KEY (idVol)
		REFERENCES Vols(idVol),
	FOREIGN KEY (idStatut)
		REFERENCES Statuts(idStatut)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE Scenarios (
	idScenario	    INT				NOT NULL AUTO_INCREMENT,
	description 	VARCHAR(100)	NOT NULL,
	
	PRIMARY KEY (idScenario)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE VolScenarios (
	idVolScenario	    INT			NOT NULL AUTO_INCREMENT,
	idVol				INT			NOT NULL,
	idScenario			INT			NOT NULL,		
	
	PRIMARY KEY (idVolScenario),
	FOREIGN KEY (idVol)
		REFERENCES Vols(idVol),
	FOREIGN KEY (idScenario)
		REFERENCES Scenarios(idScenario)
) CHARACTER SET utf8 COLLATE utf8_general_ci;

###############################################################################

INSERT INTO Types
(nom)
VALUES
('Administrateur'),
('Contrôleur');


INSERT INTO Utilisateurs
(idType,nomUtilisateur,motPasse)
VALUES
(
	1,
    'Adminstrateur temporaire',
    md5('Admin123')
);