-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Serveur: localhost
-- Généré le : Mar 25 Octobre 2016 à 17:07
-- Version du serveur: 5.5.8
-- Version de PHP: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données: `420.5a5.a16_aeroplus`
--
CREATE DATABASE `AirAmbe` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `AirAmbe`;

-- --------------------------------------------------------

--
-- Structure de la table `aeroports`
--

DROP TABLE IF EXISTS `aeroports`;
CREATE TABLE IF NOT EXISTS `aeroports` (
  `idAeroport` int(11) NOT NULL AUTO_INCREMENT,
  `ville` varchar(30) NOT NULL,
  `codeAITA` varchar(3) NOT NULL,
  PRIMARY KEY (`idAeroport`),
  UNIQUE KEY `codeAITA` (`codeAITA`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

--
-- Contenu de la table `aeroports`
--

INSERT INTO `aeroports` (`idAeroport`, `ville`, `codeAITA`) VALUES
(1, 'Montréal', 'YUL'),
(2, 'New-York', 'JFK'),
(3, 'Toronto', 'YYZ'),
(4, 'Vancouver', 'YVR'),
(5, 'Paris', 'CDG'),
(6, 'Honolulu', 'HNL'),
(7, 'Hong-Kong', 'HKG'),
(8, 'Moscou', 'SVO'),
(9, 'Los Angeles', 'LAX'),
(10, 'Sydney', 'SYD');

-- --------------------------------------------------------

--
-- Structure de la table `avions`
--

DROP TABLE IF EXISTS `avions`;
CREATE TABLE IF NOT EXISTS `avions` (
  `idAvion` int(11) NOT NULL AUTO_INCREMENT,
  `marque` varchar(30) NOT NULL,
  `modele` varchar(30) NOT NULL,
  PRIMARY KEY (`idAvion`),
  UNIQUE KEY `Avions_Marque_Modele_UK` (`marque`,`modele`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=21 ;

--
-- Contenu de la table `avions`
--

INSERT INTO `avions` (`idAvion`, `marque`, `modele`) VALUES
(1, 'Airbus', 'A320'),
(2, 'Airbus', 'A330'),
(3, 'Airbus', 'A340'),
(4, 'Airbus', 'A350 XWB'),
(5, 'Airbus', 'A380'),
(6, 'Anthonov', 'An-77'),
(7, 'Boeing', '737 MAX'),
(8, 'Boeing', '747-8'),
(9, 'Boeing', '767'),
(10, 'Boeing', '777'),
(11, 'Boeing', '777X'),
(12, 'Boeing', '787'),
(15, 'Bombardier', 'Challenger 800'),
(13, 'Bombardier', 'CS100'),
(14, 'Bombardier', 'CS300'),
(16, 'Bombardier', 'Global Express XRS'),
(17, 'Bombardier', 'Learjet 60'),
(18, 'Cessna', '120'),
(19, 'Cessna', '180'),
(20, 'Cessna', '210');

-- --------------------------------------------------------

--
-- Structure de la table `historiques`
--

DROP TABLE IF EXISTS `historiques`;
CREATE TABLE IF NOT EXISTS `historiques` (
  `idHistorique` int(11) NOT NULL AUTO_INCREMENT,
  `idVol` int(11) NOT NULL,
  `idStatut` int(11) NOT NULL,
  `dateHeure` datetime NOT NULL,
  PRIMARY KEY (`idHistorique`),
  KEY `idVol` (`idVol`),
  KEY `idStatut` (`idStatut`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Contenu de la table `historiques`
--


-- --------------------------------------------------------

--
-- Structure de la table `scenarios`
--

DROP TABLE IF EXISTS `scenarios`;
CREATE TABLE IF NOT EXISTS `scenarios` (
  `idScenario` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(100) NOT NULL,
  PRIMARY KEY (`idScenario`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=9 ;

--
-- Contenu de la table `scenarios`
--


-- --------------------------------------------------------

--
-- Structure de la table `statuts`
--

DROP TABLE IF EXISTS `statuts`;
CREATE TABLE IF NOT EXISTS `statuts` (
  `idStatut` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(30) NOT NULL,
  PRIMARY KEY (`idStatut`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Contenu de la table `statuts`
--


-- --------------------------------------------------------

--
-- Structure de la table `types`
--

DROP TABLE IF EXISTS `types`;
CREATE TABLE IF NOT EXISTS `types` (
  `idType` int(11) NOT NULL AUTO_INCREMENT,
  `typeUtilisateur` varchar(20) NOT NULL,
  PRIMARY KEY (`idType`),
  UNIQUE KEY `typeUtilisateur` (`typeUtilisateur`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `types`
--

INSERT INTO `types` (`idType`, `typeUtilisateur`) VALUES
(1, 'Administrateur'),
(2, 'Contrôleur');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateurs`
--

DROP TABLE IF EXISTS `utilisateurs`;
CREATE TABLE IF NOT EXISTS `utilisateurs` (
  `idUtilisateur` int(11) NOT NULL AUTO_INCREMENT,
  `idType` int(11) NOT NULL,
  `nomUtilisateur` varchar(30) NOT NULL,
  `motPasse` varchar(32) NOT NULL,
  `prenom` varchar(20) DEFAULT NULL,
  `nom` varchar(20) DEFAULT NULL,
  `poste` varchar(4) DEFAULT NULL,
  `dateEmbauche` date DEFAULT NULL,
  `telephone` varchar(10) DEFAULT NULL,
  `courriel` varchar(50) DEFAULT NULL,
  `adresse` varchar(50) DEFAULT NULL,
  `photo` blob,
  PRIMARY KEY (`idUtilisateur`),
  UNIQUE KEY `nomUtilisateur` (`nomUtilisateur`),
  KEY `idType` (`idType`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=16 ;

--
-- Contenu de la table `utilisateurs`
--

INSERT INTO `utilisateurs` (`idUtilisateur`, `idType`, `nomUtilisateur`, `motPasse`, `prenom`, `nom`, `poste`, `dateEmbauche`, `telephone`, `courriel`, `adresse`, `photo`) VALUES
(1, 2, 'oprovost', '7215ee9c7d9dc229d2921a40e899ec5f', 'Olivier', 'Provost', '2220', '2011-12-20', '4504326969', 'oprovost@airambe.com', '1251 rue Fournier, Saint-Jérôme, Qc', NULL),
(2, 1, 'vdésilets', 'f02368945726d5fc2a14eb576f7276c0', 'Vincent', 'Désilets', '2223', '2014-02-12', '4383934624', 'vdésilets@airambe.com', '235 rue Tour du Lac, Saint-Faustin Lac-Carré, Qc', NULL),
(3, 2, 'amasse', 'd8578edf8458ce06fbc5bb76a58c5ca4', 'Anthony', 'Massé', '2221', '2012-01-22', '4382154564', 'amasse@harambe.com', '455 rue du Gorille, Mirabel, Qc', NULL),
(14, 1, 'alainadmin', '827ccb0eea8a706c4c34a16891f84e7b', 'Alain', 'Martel', '9999', '2016-10-25', '4504360615', 'amartel@airambe.com', 'Rue du Gorille', NULL),
(15, 2, 'alaincontroleur', '827ccb0eea8a706c4c34a16891f84e7b', 'Alain', 'Martel', '9999', '2016-10-25', '4504360615', 'amartel1@airambe.com', 'Rue du Gorille', NULL);

-- --------------------------------------------------------

--
-- Structure de la table `vols`
--

DROP TABLE IF EXISTS `vols`;
CREATE TABLE IF NOT EXISTS `vols` (
  `idVol` int(11) NOT NULL AUTO_INCREMENT,
  `idAvion` int(11) NOT NULL,
  `idAeroport` int(11) NOT NULL,
  `numeroVol` varchar(10) NOT NULL,
  `estAtterrissage` tinyint(1) NOT NULL,
  PRIMARY KEY (`idVol`),
  UNIQUE KEY `numeroVol` (`numeroVol`),
  KEY `idAvion` (`idAvion`),
  KEY `idAeroport` (`idAeroport`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=22 ;

--
-- Contenu de la table `vols`
--

INSERT INTO `vols` (`idVol`, `idAvion`, `idAeroport`, `numeroVol`, `estAtterrissage`) VALUES
(1, 1, 1, 'AC893', 1),
(2, 2, 2, 'TS701', 1),
(3, 3, 3, 'LH478', 1),
(4, 4, 4, 'DL3660', 1),
(5, 5, 5, 'AC8980', 1),
(6, 6, 6, 'AC416', 1),
(7, 7, 7, 'PD423', 1),
(8, 8, 8, 'WS3494', 1),
(9, 9, 9, 'AA4196', 1),
(10, 10, 10, 'WG520', 1),
(11, 11, 1, 'KL671', 0),
(12, 12, 2, 'WS590', 0),
(13, 13, 3, 'TS469', 0),
(14, 14, 4, 'AC8769', 0),
(15, 15, 5, 'BA95', 0),
(16, 16, 6, 'AF342', 0),
(17, 17, 7, 'QR763', 0),
(18, 18, 8, 'OP2212', 0),
(19, 19, 9, 'VD911', 0),
(20, 20, 10, 'AM2309', 0);

-- --------------------------------------------------------

--
-- Structure de la table `volscenarios`
--

DROP TABLE IF EXISTS `volscenarios`;
CREATE TABLE IF NOT EXISTS `volscenarios` (
  `idVolScenario` int(11) NOT NULL AUTO_INCREMENT,
  `idVol` int(11) NOT NULL,
  `idScenario` int(11) NOT NULL,
  PRIMARY KEY (`idVolScenario`),
  KEY `idVol` (`idVol`),
  KEY `idScenario` (`idScenario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Contenu de la table `volscenarios`
--


--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `historiques`
--
ALTER TABLE `historiques`
  ADD CONSTRAINT `historiques_ibfk_1` FOREIGN KEY (`idVol`) REFERENCES `vols` (`idVol`),
  ADD CONSTRAINT `historiques_ibfk_2` FOREIGN KEY (`idStatut`) REFERENCES `statuts` (`idStatut`);

--
-- Contraintes pour la table `utilisateurs`
--
ALTER TABLE `utilisateurs`
  ADD CONSTRAINT `utilisateurs_ibfk_1` FOREIGN KEY (`idType`) REFERENCES `types` (`idType`);

--
-- Contraintes pour la table `vols`
--
ALTER TABLE `vols`
  ADD CONSTRAINT `vols_ibfk_1` FOREIGN KEY (`idAvion`) REFERENCES `avions` (`idAvion`),
  ADD CONSTRAINT `vols_ibfk_2` FOREIGN KEY (`idAeroport`) REFERENCES `aeroports` (`idAeroport`);

--
-- Contraintes pour la table `volscenarios`
--
ALTER TABLE `volscenarios`
  ADD CONSTRAINT `volscenarios_ibfk_1` FOREIGN KEY (`idVol`) REFERENCES `vols` (`idVol`),
  ADD CONSTRAINT `volscenarios_ibfk_2` FOREIGN KEY (`idScenario`) REFERENCES `scenarios` (`idScenario`);
