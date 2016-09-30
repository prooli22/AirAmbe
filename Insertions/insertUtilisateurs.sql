
#Insert dans la table Adresses
INSERT INTO Adresses
(numero,rue,ville,province,codePostal)
VALUES
(
	235,
    'Tour du Lac',
    'Saint-Faustin Lac-Carrée',
    'QC',
    'J0T1J0'
);

INSERT INTO Adresses
(numero,rue,ville,province,codePostal)
VALUES
(
	1251,
    'Fournier',
    'Saint-Jérôme',
    'QC',
    'J7N1R4'
);

INSERT INTO Adresses
(numero,rue,ville,province,codePostal)
VALUES
(
	455,
    'Rue du Gorille',
    'Mirabel',
    'QC',
    'J7Z1M2'
);

#Insert dans la table Utilisateurs

INSERT INTO Utilisateurs
(idAdresse,idType,nomUtilisateur,motPasse,prenom,nom,poste,dateEmbauche,numeroTelephone,courriel)
VALUES
(
	2,
    1,
    'oprovost',
    md5('ecotopia++'),
    'Olivier',
    'Provost',
    '2220',
    '2011-12-20',
    '4504326969',
    'oprovost@harambe.com'
),
(
	1,
    2,
    'vdesilets',
    md5('bonjour'),
    'Vincent',
    'Désilets',
    '2222',
    '2014-02-12',
    '4383934624',
    'vdesilets@harambe.com'
),
(
	3,
    2,
    'amasse',
    md5('qwerty'),
    'Anthony',
    'Massé',
    '2221',
    '2012-01-22',
    '4382154564',
    'amasse@harambe.com'
);









































