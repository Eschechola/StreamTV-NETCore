-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Nov 07, 2019 at 05:20 PM
-- Server version: 8.0.18
-- PHP Version: 7.2.24-0ubuntu0.18.04.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `senaitvs`
--
CREATE DATABASE IF NOT EXISTS `senaitvs`;
USE `senaitvs`;

-- --------------------------------------------------------

--
-- Table structure for table `cliente`
--

CREATE TABLE `cliente` (
  `id` int(11) NOT NULL,
  `instituicao` varchar(200) DEFAULT NULL,
  `responsavel` varchar(200) DEFAULT NULL,
  `senha` varchar(100) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `endereco` varchar(300) DEFAULT NULL,
  `email` varchar(200) DEFAULT NULL,
  `qt_televisoes` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `Televisoes`
--

CREATE TABLE `Televisoes` (
  `id` int(11) NOT NULL,
  `fk_id_cliente` int(11) DEFAULT NULL,
  `nome` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `Videos`
--

CREATE TABLE `Videos` (
  `id` int(11) NOT NULL,
  `fk_id_televisao` int(11) DEFAULT NULL,
  `url` varchar(300) DEFAULT NULL,
  `modificado` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `Televisoes`
--
ALTER TABLE `Televisoes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_id_cliente` (`fk_id_cliente`);

--
-- Indexes for table `Videos`
--
ALTER TABLE `Videos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_id_televisao` (`fk_id_televisao`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cliente`
--
ALTER TABLE `cliente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Televisoes`
--
ALTER TABLE `Televisoes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Videos`
--
ALTER TABLE `Videos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Televisoes`
--
ALTER TABLE `Televisoes`
  ADD CONSTRAINT `fk_id_cliente` FOREIGN KEY (`fk_id_cliente`) REFERENCES `cliente` (`id`);

--
-- Constraints for table `Videos`
--
ALTER TABLE `Videos`
  ADD CONSTRAINT `fk_id_televisao` FOREIGN KEY (`fk_id_televisao`) REFERENCES `Televisoes` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
