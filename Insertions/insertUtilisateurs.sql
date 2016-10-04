INSERT INTO Utilisateurs
(idType, nomUtilisateur, motPasse, prenom, nom, poste, dateEmbauche, numeroTelephone, courriel, adresse)
VALUES
(
    1,
    'oprovost',
    md5('ecotopia++'),
    'Olivier',
    'Provost',
    '2220',
    '2011-12-20',
    '4504326969',
    'oprovost@harambe.com',
	'1251 rue Fournier, Saint-Jérôme, Qc'
),
(
    2,
    'vdesilets',
    md5('bonjour'),
    'Vincent',
    'Désilets',
    '2222',
    '2014-02-12',
    '4383934624',
    'vdesilets@harambe.com',
	'235 rue Tour du Lac, Saint-Faustin Lac-Carré, Qc'
),
(
    2,
    'amasse',
    md5('qwerty'),
    'Anthony',
    'Massé',
    '2221',
    '2012-01-22',
    '4382154564',
    'amasse@harambe.com',
	'455 rue du Gorille, Mirabel, Qc'
);
