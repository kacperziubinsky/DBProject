-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 28 Sty 2025, 23:04
-- Wersja serwera: 10.4.27-MariaDB
-- Wersja PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `ziubinsk_dbproject`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `assets`
--

CREATE TABLE `assets` (
  `AssetID` int(11) NOT NULL,
  `AssetName` varchar(100) DEFAULT NULL,
  `LocationID` int(11) DEFAULT NULL,
  `PurchaseDate` date DEFAULT NULL,
  `Value` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `assets`
--

INSERT INTO `assets` (`AssetID`, `AssetName`, `LocationID`, `PurchaseDate`, `Value`) VALUES
(1, 'Laptop Dell', 1, '2022-01-10', '3500.00'),
(2, 'Projektor Epson', 2, '2021-12-05', '2000.00'),
(5, 'Laptop Dell', 1, '2023-01-15', '3500.00'),
(6, 'Projektor Epson', 2, '2023-02-05', '2000.00'),
(7, 'Smartphone Samsung', 1, '2023-03-12', '1500.00'),
(8, 'Monitor LG', 2, '2023-04-20', '1000.00'),
(9, 'Drukarka HP', 1, '2023-05-18', '800.00'),
(10, 'Mikrofon Audio-Technica', 2, '2023-06-15', '350.00'),
(11, 'Kamera Canon', 1, '2023-07-22', '2500.00'),
(12, 'Tablet Apple', 2, '2023-08-10', '1200.00'),
(13, 'Rzutnik Optoma', 1, '2023-09-18', '1800.00'),
(14, 'Dysk SSD Samsung', 2, '2023-10-05', '500.00'),
(15, 'Laptop Lenovo', 1, '2023-11-15', '3300.00'),
(16, 'Projektor Benq', 2, '2023-12-10', '2200.00'),
(17, 'Router TP-Link', 1, '2024-01-15', '400.00'),
(18, 'Aparat Nikon', 2, '2024-02-10', '2200.00'),
(19, 'Smartwatch Garmin', 1, '2024-03-25', '600.00'),
(20, 'Laptop Asus', 2, '2024-04-05', '2900.00'),
(21, 'Mysz Logitech', 1, '2024-05-02', '150.00'),
(22, 'Kamera GoPro', 2, '2024-06-17', '1500.00'),
(23, 'Głośnik JBL', 1, '2024-07-01', '400.00'),
(24, 'Zasilacz UPS', 2, '2024-08-10', '750.00'),
(25, 'Monitor Samsung', 1, '2024-09-15', '1100.00'),
(26, 'Laptop HP', 2, '2024-10-12', '2800.00'),
(27, 'Rzutnik ViewSonic', 1, '2024-11-05', '1900.00'),
(28, 'Słuchawki Bose', 2, '2024-12-22', '700.00'),
(29, 'Tablet Lenovo', 1, '2025-01-10', '950.00'),
(30, 'Mysz Razer', 2, '2025-01-18', '250.00'),
(31, 'Kamera Sony', 1, '2025-01-25', '2200.00'),
(32, 'Laptop Acer', 2, '2025-01-05', '2700.00'),
(33, 'Skaner Fujitsu', 1, '2023-01-10', '900.00'),
(34, 'Monitor Dell', 2, '2023-02-14', '1300.00'),
(35, 'Projektor Kodak', 1, '2023-03-03', '2100.00'),
(36, 'Rzutnik Philips', 2, '2023-04-15', '1500.00'),
(37, 'Telefon Xiaomi', 1, '2023-05-12', '950.00'),
(38, 'Laptop MSI', 2, '2023-06-18', '3400.00'),
(39, 'Głośnik Sony', 1, '2023-07-07', '500.00'),
(40, 'Zewnętrzny dysk Seagate', 2, '2023-08-20', '400.00'),
(41, 'Mikrofon Blue Yeti', 1, '2023-09-12', '500.00'),
(42, 'Projektor Vivitek', 2, '2023-10-01', '2000.00'),
(43, 'Smartphone Huawei', 1, '2023-11-25', '1200.00'),
(44, 'Aparat Panasonic', 2, '2023-12-20', '1800.00'),
(45, 'Drukarka Canon', 1, '2024-01-08', '1000.00'),
(46, 'Aparat FujiFilm', 2, '2024-02-03', '2000.00'),
(47, 'Tablet Samsung', 1, '2024-03-22', '1100.00'),
(48, 'Smartwatch Samsung', 2, '2024-04-15', '500.00'),
(49, 'Laptop Razer', 1, '2024-05-30', '4000.00'),
(50, 'Rzutnik BenQ', 2, '2024-06-10', '2300.00'),
(51, 'Router Netgear', 1, '2024-07-20', '350.00'),
(52, 'Monitor Philips', 2, '2024-08-28', '1000.00'),
(53, 'Kamera Olympus', 1, '2024-09-15', '2500.00'),
(54, 'Dysk SSD Kingston', 2, '2024-10-08', '450.00'),
(55, 'Smartphone Nokia', 1, '2024-11-01', '800.00'),
(56, 'Projektor Panasonic', 2, '2024-12-22', '1800.00'),
(57, 'Laptop Huawei', 1, '2025-01-12', '3000.00'),
(58, 'Monitor ViewSonic', 2, '2025-01-20', '1000.00'),
(59, 'Głośnik Marshall', 1, '2025-01-28', '450.00'),
(60, 'Projektor JVC', 2, '2025-01-09', '1700.00'),
(61, 'Router Cisco', 1, '2025-01-25', '800.00'),
(62, 'Słuchawki Sony', 2, '2025-01-03', '550.00'),
(63, 'Laptop Microsoft', 1, '2025-01-07', '3200.00'),
(64, 'Drukarka Xerox', 2, '2025-01-16', '600.00'),
(65, 'Mysz Logitech', 1, '2025-01-22', '100.00'),
(66, 'Laptop Toshiba', 2, '2025-01-11', '2800.00'),
(67, 'Smartphone Apple', 1, '2025-01-14', '1200.00'),
(68, 'Tablet Huawei', 2, '2025-01-19', '950.00'),
(69, 'Głośnik Bose', 1, '2025-01-21', '650.00'),
(70, 'Mikrofon Rode', 2, '2025-01-23', '400.00'),
(71, 'Smartwatch Apple', 1, '2025-01-26', '500.00'),
(72, 'Projektor LG', 2, '2025-01-30', '1600.00');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `clients`
--

CREATE TABLE `clients` (
  `ClientID` int(11) NOT NULL,
  `ClientName` varchar(100) DEFAULT NULL,
  `ContactEmail` varchar(100) DEFAULT NULL,
  `ContactPhone` varchar(15) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `clients`
--

INSERT INTO `clients` (`ClientID`, `ClientName`, `ContactEmail`, `ContactPhone`) VALUES
(1, 'Firma ABC', 'kontakt@abc.pl', '1122334455'),
(2, 'Firma XYZ', 'kontakt@xyz.pl', '5566778899'),
(3, 'string', 'string', 'string'),
(5, 'Firma A', 'firmaA@abc.com', '111111111'),
(6, 'Firma B', 'firmaB@xyz.com', '222222222'),
(7, 'Firma C', 'firmaC@xyz.com', '333333333'),
(8, 'Firma D', 'firmaD@abc.com', '444444444'),
(9, 'Firma E', 'firmaE@xyz.com', '555555555'),
(10, 'Firma F', 'firmaF@abc.com', '666666666'),
(11, 'Firma G', 'firmaG@xyz.com', '777777777'),
(12, 'Firma H', 'firmaH@abc.com', '888888888'),
(13, 'Firma I', 'firmaI@xyz.com', '999999999'),
(14, 'Firma J', 'firmaJ@abc.com', '100000000'),
(15, 'Firma K', 'firmaK@xyz.com', '111111112'),
(16, 'Firma L', 'firmaL@abc.com', '121212121'),
(17, 'Firma M', 'firmaM@xyz.com', '131313131'),
(18, 'Firma N', 'firmaN@abc.com', '141414141'),
(19, 'Firma O', 'firmaO@xyz.com', '151515151'),
(20, 'Firma P', 'firmaP@abc.com', '161616161'),
(21, 'Firma Q', 'firmaQ@xyz.com', '171717171'),
(22, 'Firma R', 'firmaR@abc.com', '181818181'),
(23, 'Firma S', 'firmaS@xyz.com', '191919191'),
(24, 'Firma T', 'firmaT@abc.com', '202020202'),
(25, 'Firma U', 'firmaU@xyz.com', '212121212'),
(26, 'Firma V', 'firmaV@abc.com', '222222222'),
(27, 'Firma W', 'firmaW@xyz.com', '232323232'),
(28, 'Firma X', 'firmaX@abc.com', '242424242'),
(29, 'Firma Y', 'firmaY@xyz.com', '252525252'),
(30, 'Firma Z', 'firmaZ@abc.com', '262626262'),
(31, 'Firma AA', 'firmaAA@xyz.com', '272727272'),
(32, 'Firma BB', 'firmaBB@abc.com', '282828282'),
(33, 'Firma CC', 'firmaCC@xyz.com', '292929292');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `departments`
--

CREATE TABLE `departments` (
  `DepartmentID` int(11) NOT NULL,
  `DepartmentName` varchar(50) DEFAULT NULL,
  `ManagerID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `departments`
--

INSERT INTO `departments` (`DepartmentID`, `DepartmentName`, `ManagerID`) VALUES
(1, 'HR', 1),
(2, 'IT', 2),
(3, 'Finance', 3),
(6, 'Marketing', 4),
(7, 'Sales', 5);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `employees`
--

CREATE TABLE `employees` (
  `EmployeeID` int(11) NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `DepartmentID` int(11) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Phone` varchar(15) DEFAULT NULL,
  `HireDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `employees`
--

INSERT INTO `employees` (`EmployeeID`, `FirstName`, `LastName`, `DepartmentID`, `Email`, `Phone`, `HireDate`) VALUES
(1, 'Anna', 'Kowalska', 1, 'anna.kowalska@firma.pl', '123456789', '2020-05-15'),
(2, 'Jan', 'Nowak', 2, 'jan.nowak@firma.pl', '987654321', '2018-11-20'),
(3, 'Maria', 'Wiśniewska', 3, 'maria.wisniewska@firma.pl', '555666777', '2021-03-10'),
(4, 'Anna', 'Kowalska', 1, 'anna.kowalska@firma.pl', '123456789', '2020-05-15'),
(5, 'Jan', 'Nowak', 2, 'jan.nowak@firma.pl', '987654321', '2018-11-20'),
(6, 'Maria', 'Wiśniewska', 3, 'maria.wisniewska@firma.pl', '555666777', '2021-03-10'),
(7, 'Kamil', 'Mazur', 1, 'kamil.mazur@firma.pl', '1122334455', '2022-01-15'),
(8, 'Ola', 'Jankowska', 4, 'ola.jankowska@firma.pl', '6677889900', '2022-07-21'),
(9, 'Marek', 'Kwiatkowski', 5, 'marek.kwiatkowski@firma.pl', '3344556677', '2023-04-05'),
(10, 'Zuzanna', 'Nowicka', 2, 'zuzanna.nowicka@firma.pl', '9988776655', '2020-09-10'),
(11, 'Tomasz', 'Wójcik', 3, 'tomasz.wojcik@firma.pl', '4433221100', '2019-02-25'),
(12, 'Patryk', 'Kaczmarek', 4, 'patryk.kaczmarek@firma.pl', '7766554433', '2021-06-30'),
(13, 'Karolina', 'Sienkiewicz', 5, 'karolina.sienkiewicz@firma.pl', '1122339988', '2022-12-01'),
(14, 'Piotr', 'Kamiński', 1, 'piotr.kaminski@firma.pl', '5566772233', '2018-08-15'),
(15, 'Michał', 'Wysocki', 2, 'michal.wysocki@firma.pl', '6677883344', '2021-04-10'),
(16, 'Weronika', 'Bąk', 3, 'weronika.bak@firma.pl', '8899001122', '2020-03-12'),
(17, 'Szymon', 'Górski', 4, 'szymon.gorski@firma.pl', '4433665566', '2023-01-15'),
(18, 'Julia', 'Zalewska', 5, 'julia.zalewska@firma.pl', '3355442233', '2022-09-20'),
(19, 'Sebastian', 'Krawczyk', 1, 'sebastian.krawczyk@firma.pl', '5599773322', '2019-11-10'),
(20, 'Daria', 'Bielska', 2, 'daria.bielska@firma.pl', '8877665544', '2020-12-01'),
(21, 'Adrian', 'Lis', 3, 'adrian.lis@firma.pl', '2233445566', '2021-07-14'),
(22, 'Barbara', 'Król', 4, 'barbara.krol@firma.pl', '5566778888', '2023-02-10'),
(23, 'Wojciech', 'Kowal', 5, 'wojciech.kowal@firma.pl', '7766554433', '2020-11-25'),
(24, 'Kinga', 'Miller', 1, 'kinga.miller@firma.pl', '9988772233', '2022-06-15'),
(25, 'Krzysztof', 'Duda', 2, 'krzysztof.duda@firma.pl', '4433221100', '2018-10-05'),
(26, 'Aleksandra', 'Chmiel', 3, 'aleksandra.chmiel@firma.pl', '3344556677', '2021-01-20'),
(27, 'Mariusz', 'Błaszczyk', 4, 'mariusz.blaszczyk@firma.pl', '1122334455', '2022-02-05'),
(28, 'Daniel', 'Zawisza', 5, 'daniel.zawisza@firma.pl', '7788993344', '2023-05-10'),
(29, 'Beata', 'Gajewska', 1, 'beata.gajewska@firma.pl', '5566774455', '2021-03-25'),
(30, 'Sonia', 'Jezior', 2, 'sonia.jezior@firma.pl', '8899776655', '2022-07-18'),
(31, 'Rafał', 'Wesołowski', 3, 'rafal.wesolowski@firma.pl', '2233445566', '2020-04-10'),
(32, 'Dawid', 'Stolarz', 4, 'dawid.stolarz@firma.pl', '5566772233', '2023-01-08'),
(33, 'Emilia', 'Frycz', 5, 'emilia.frycz@firma.pl', '3344556677', '2022-08-15');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `invoices`
--

CREATE TABLE `invoices` (
  `InvoiceID` int(11) NOT NULL,
  `ClientID` int(11) DEFAULT NULL,
  `InvoiceDate` date DEFAULT NULL,
  `TotalAmount` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `invoices`
--

INSERT INTO `invoices` (`InvoiceID`, `ClientID`, `InvoiceDate`, `TotalAmount`) VALUES
(1, 1, '2023-01-15', '10000.00'),
(2, 2, '2023-01-20', '15000.00'),
(3, 1, '2023-01-10', '1200.00'),
(4, 2, '2023-01-15', '1500.00'),
(5, 3, '2023-01-20', '2000.00'),
(6, 5, '2023-01-25', '1800.00'),
(7, 6, '2023-02-05', '1350.00'),
(8, 7, '2023-02-10', '1450.00'),
(9, 8, '2023-02-15', '1750.00'),
(10, 9, '2023-02-20', '1600.00'),
(11, 10, '2023-03-01', '2200.00'),
(12, 11, '2023-03-05', '2500.00'),
(13, 12, '2023-04-10', '1300.00'),
(14, 13, '2023-04-12', '1700.00'),
(15, 14, '2023-04-20', '1900.00'),
(16, 15, '2023-04-22', '1600.00'),
(17, 16, '2023-05-02', '2100.00'),
(18, 17, '2023-05-08', '1800.00'),
(19, 18, '2023-05-15', '2300.00'),
(20, 19, '2023-05-18', '1500.00'),
(21, 20, '2023-06-01', '2500.00'),
(22, 21, '2023-06-07', '2200.00'),
(23, 22, '2023-07-01', '2400.00'),
(24, 23, '2023-07-10', '2000.00'),
(25, 24, '2023-07-12', '2100.00'),
(26, 25, '2023-07-18', '1800.00'),
(27, 26, '2023-08-01', '1900.00'),
(28, 27, '2023-08-05', '2400.00'),
(29, 28, '2023-08-12', '2600.00'),
(30, 29, '2023-08-18', '2300.00'),
(31, 30, '2023-09-01', '1500.00'),
(32, 31, '2023-09-08', '2200.00'),
(33, 32, '2023-10-01', '1800.00'),
(34, 33, '2023-10-03', '2000.00'),
(35, 1, '2023-10-15', '2300.00'),
(36, 2, '2023-10-20', '2100.00'),
(37, 3, '2023-11-01', '2500.00'),
(38, 5, '2023-11-05', '2200.00'),
(39, 6, '2023-11-10', '2600.00'),
(40, 7, '2023-11-15', '2400.00'),
(41, 8, '2023-12-01', '2700.00'),
(42, 9, '2023-12-05', '2800.00'),
(43, 10, '2024-01-01', '1500.00'),
(44, 11, '2024-01-05', '2000.00'),
(45, 12, '2024-01-10', '1800.00'),
(46, 13, '2024-01-15', '1700.00'),
(47, 14, '2024-02-01', '2200.00'),
(48, 15, '2024-02-05', '2300.00'),
(49, 16, '2024-02-10', '2100.00'),
(50, 17, '2024-02-15', '2400.00'),
(51, 18, '2024-03-01', '2500.00'),
(52, 19, '2024-03-05', '2200.00'),
(53, 20, '2024-04-01', '2600.00'),
(54, 21, '2024-04-07', '2300.00'),
(55, 22, '2024-04-12', '2700.00'),
(56, 23, '2024-04-15', '2500.00'),
(57, 24, '2024-05-01', '2800.00'),
(58, 25, '2024-05-06', '2600.00'),
(59, 26, '2024-05-10', '2900.00'),
(60, 27, '2024-05-15', '3000.00'),
(61, 28, '2024-06-01', '3200.00'),
(62, 29, '2024-06-05', '3100.00'),
(63, 30, '2024-07-01', '3500.00'),
(64, 31, '2024-07-05', '3300.00'),
(65, 32, '2024-07-10', '3600.00'),
(66, 33, '2024-07-15', '3400.00'),
(67, 1, '2024-08-01', '3700.00'),
(68, 2, '2024-08-05', '3500.00'),
(69, 3, '2024-08-10', '3800.00'),
(70, 5, '2024-08-15', '3600.00'),
(71, 6, '2024-09-01', '3900.00'),
(72, 7, '2024-09-05', '3800.00'),
(73, 8, '2024-10-01', '4100.00'),
(74, 9, '2024-10-05', '4000.00'),
(75, 10, '2024-10-10', '4200.00'),
(76, 11, '2024-10-15', '4000.00'),
(77, 12, '2024-11-01', '4300.00'),
(78, 13, '2024-11-05', '4100.00'),
(79, 14, '2024-11-10', '4400.00'),
(80, 15, '2024-11-15', '4200.00'),
(81, 16, '2024-12-01', '4500.00'),
(82, 17, '2024-12-05', '4600.00');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `locations`
--

CREATE TABLE `locations` (
  `LocationID` int(11) NOT NULL,
  `LocationName` varchar(100) DEFAULT NULL,
  `Address` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `locations`
--

INSERT INTO `locations` (`LocationID`, `LocationName`, `Address`) VALUES
(1, 'Biuro Warszawa', 'ul. Marszałkowska 10, Warszawa'),
(2, 'Biuro Kraków', 'ul. Długa 20, Kraków');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `meetings`
--

CREATE TABLE `meetings` (
  `MeetingID` int(11) NOT NULL,
  `MeetingDate` date DEFAULT NULL,
  `Subject` varchar(100) DEFAULT NULL,
  `Participants` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `meetings`
--

INSERT INTO `meetings` (`MeetingID`, `MeetingDate`, `Subject`, `Participants`) VALUES
(1, '2023-01-25', 'Omówienie projektu HR', 'Anna Kowalska, Jan Nowak'),
(2, '2023-01-30', 'Postępy w E-commerce', 'Jan Nowak, Maria Wiśniewska');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `projects`
--

CREATE TABLE `projects` (
  `ProjectID` int(11) NOT NULL,
  `ProjectName` varchar(100) DEFAULT NULL,
  `DepartmentID` int(11) DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `projects`
--

INSERT INTO `projects` (`ProjectID`, `ProjectName`, `DepartmentID`, `StartDate`, `EndDate`) VALUES
(1, 'System HR', 1, '2023-01-01', '2023-12-31'),
(2, 'E-commerce Platform', 2, '2022-06-01', '2023-06-01'),
(3, 'Financial Dashboard', 3, '2023-03-01', '2023-12-31'),
(6, 'Aplikacja Mobilna', 2, '2023-05-01', '2023-12-31'),
(7, 'Projekt Optymalizacji', 3, '2023-06-01', '2023-12-31'),
(8, 'Infrastruktura IT', 2, '2023-07-01', '2023-12-31'),
(9, 'Automatyzacja Procesów', 3, '2023-08-01', '2024-01-31'),
(10, 'System CRM', 1, '2023-09-01', '2024-06-30'),
(11, 'Projekt AI', 2, '2023-10-01', '2024-12-31'),
(12, 'Zarządzanie Zasobami', 1, '2023-11-01', '2024-10-31'),
(13, 'Nowy ERP', 3, '2023-12-01', '2024-06-30'),
(14, 'Bezpieczeństwo Sieci', 2, '2024-01-01', '2024-12-31'),
(15, 'Rebranding Firmy', 1, '2024-02-01', '2024-12-31'),
(16, 'Projekt B2B', 3, '2024-03-01', '2024-12-31'),
(17, 'SaaS Platforma', 2, '2024-04-01', '2025-03-31'),
(18, 'Przemiany Digitalowe', 1, '2024-05-01', '2024-12-31'),
(19, 'System Analytics', 2, '2024-06-01', '2024-12-31'),
(20, 'Nowy CRM', 3, '2024-07-01', '2025-06-30'),
(21, 'Zarządzanie Danymi', 2, '2024-08-01', '2025-01-31'),
(22, 'Integracja Aplikacji', 1, '2024-09-01', '2025-06-30'),
(23, 'Rozwój API', 3, '2024-10-01', '2025-01-31'),
(24, 'Optymalizacja Procesów', 1, '2024-11-01', '2025-06-30'),
(25, 'Nowy Wdrożenie IT', 2, '2024-12-01', '2025-12-31'),
(26, 'Zarządzanie Zasobami Ludzkimi', 1, '2025-01-01', '2025-12-31'),
(27, 'Rekonstrukcja Strony', 2, '2025-02-01', '2025-12-31'),
(28, 'Nowe Biuro IT', 3, '2025-03-01', '2025-12-31'),
(29, 'Wdrożenie ERP', 1, '2025-04-01', '2025-12-31'),
(30, 'Digitalizacja Danych', 2, '2025-05-01', '2025-12-31'),
(31, 'Projekt Monitorowania', 1, '2025-06-01', '2025-12-31'),
(32, 'Modyfikacja Aplikacji', 2, '2025-07-01', '2025-12-31'),
(33, 'Nowe Usługi Webowe', 3, '2025-08-01', '2025-12-31'),
(34, 'Automatyzacja Marketingu', 1, '2025-09-01', '2025-12-31'),
(35, 'Rozwój Systemu HR', 2, '2025-10-01', '2025-12-31'),
(36, 'Infrastruktura Serwerowa', 3, '2025-11-01', '2025-12-31'),
(37, 'Nowe Systemy IT', 1, '2025-12-01', '2025-12-31'),
(38, 'Wdrożenie Systemu B2B', 2, '2025-01-01', '2025-12-31'),
(39, 'Aktualizacja Strony', 3, '2025-02-01', '2025-12-31'),
(40, 'Współpraca z Firmami Zewnętrznymi', 1, '2025-03-01', '2025-12-31'),
(41, 'Zarządzanie Wydajnością', 2, '2025-04-01', '2025-12-31'),
(42, 'Nowe Centrum Danych', 3, '2025-05-01', '2025-12-31'),
(43, 'Optymalizacja CRM', 1, '2025-06-01', '2025-12-31'),
(44, 'Migracja do Chmury', 2, '2025-07-01', '2025-12-31'),
(45, 'Integracja z Zewnętrznymi Systemami', 3, '2025-08-01', '2025-12-31'),
(46, 'Nowa Platforma B2B', 1, '2025-09-01', '2025-12-31'),
(47, 'Automatyzacja Procesów HR', 2, '2025-10-01', '2025-12-31'),
(48, 'Rozwój Systemów FinTech', 3, '2025-11-01', '2025-12-31'),
(49, 'Aktualizacja Oprogramowania', 1, '2025-12-01', '2025-12-31'),
(50, 'Nowe Inicjatywy IT', 2, '2025-01-01', '2025-12-31');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `salaries`
--

CREATE TABLE `salaries` (
  `SalaryID` int(11) NOT NULL,
  `EmployeeID` int(11) DEFAULT NULL,
  `MonthlySalary` decimal(10,2) DEFAULT NULL,
  `PaymentDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `salaries`
--

INSERT INTO `salaries` (`SalaryID`, `EmployeeID`, `MonthlySalary`, `PaymentDate`) VALUES
(1, 1, '5000.00', '2023-01-31'),
(2, 2, '6000.00', '2023-01-31'),
(3, 3, '5500.00', '2023-01-31'),
(7, 7, '5700.00', '2023-01-31'),
(8, 8, '5000.00', '2023-01-31'),
(9, 9, '6500.00', '2023-01-31'),
(10, 10, '7000.00', '2023-01-31'),
(11, 11, '5500.00', '2023-01-31'),
(12, 12, '5600.00', '2023-01-31'),
(13, 13, '5400.00', '2023-01-31'),
(14, 14, '4800.00', '2023-01-31'),
(15, 15, '5100.00', '2023-01-31'),
(16, 16, '5000.00', '2023-01-31'),
(17, 17, '5200.00', '2023-01-31'),
(18, 18, '5400.00', '2023-01-31'),
(19, 19, '5300.00', '2023-01-31'),
(20, 20, '5500.00', '2023-01-31'),
(21, 21, '4800.00', '2023-01-31'),
(22, 22, '4900.00', '2023-01-31'),
(23, 23, '5100.00', '2023-01-31'),
(24, 24, '4600.00', '2023-01-31'),
(25, 25, '5000.00', '2023-01-31'),
(26, 26, '5700.00', '2023-01-31'),
(27, 27, '6200.00', '2023-01-31'),
(28, 28, '6500.00', '2023-01-31'),
(29, 29, '6700.00', '2023-01-31'),
(30, 30, '6900.00', '2023-01-31');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `tasks`
--

CREATE TABLE `tasks` (
  `TaskID` int(11) NOT NULL,
  `TaskName` varchar(100) DEFAULT NULL,
  `ProjectID` int(11) DEFAULT NULL,
  `AssignedTo` int(11) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `DueDate` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Zrzut danych tabeli `tasks`
--

INSERT INTO `tasks` (`TaskID`, `TaskName`, `ProjectID`, `AssignedTo`, `Status`, `DueDate`) VALUES
(1, 'Analiza wymagań', 1, 1, 'In Progress', '2023-02-28'),
(2, 'Projekt bazy danych', 2, 2, 'Completed', '2023-01-15'),
(3, 'Raport miesięczny', 3, 3, 'Pending', '2023-03-15'),
(6, 'Analiza finansowa', 3, 6, 'Pending', '2023-06-01'),
(7, 'Przeprowadzenie spotkania z klientem', 4, 7, 'Completed', '2023-01-30'),
(8, 'Integracja systemu z API', 4, 8, 'In Progress', '2023-03-01'),
(9, 'Szkolenie dla pracowników', 5, 9, 'Pending', '2023-02-25'),
(10, 'Zarządzanie budżetem', 5, 10, 'Completed', '2023-01-28'),
(11, 'Przygotowanie dokumentacji', 6, 11, 'In Progress', '2023-02-15'),
(12, 'Analiza rynku', 6, 12, 'Pending', '2023-03-30'),
(13, 'Optymalizacja procesów', 7, 13, 'Completed', '2023-02-18'),
(14, 'Przeprowadzenie testów jednostkowych', 7, 14, 'In Progress', '2023-04-10'),
(15, 'Przygotowanie prezentacji', 8, 15, 'Pending', '2023-05-01'),
(16, 'Wdrażanie nowych funkcjonalności', 8, 16, 'In Progress', '2023-03-22'),
(17, 'Przeprowadzenie analizy SWOT', 9, 17, 'Completed', '2023-01-10'),
(18, 'Zarządzanie zasobami', 9, 18, 'In Progress', '2023-04-20'),
(19, 'Rozwój strategii marketingowej', 10, 19, 'Pending', '2023-06-15'),
(20, 'Optymalizacja SEO', 10, 20, 'Completed', '2023-01-25'),
(21, 'Koordynacja zespołu', 11, 21, 'In Progress', '2023-02-12'),
(22, 'Zarządzanie ryzykiem', 11, 22, 'Pending', '2023-04-28'),
(23, 'Analiza danych', 12, 23, 'Completed', '2023-02-08'),
(24, 'Ulepszanie UX/UI', 12, 24, 'In Progress', '2023-03-18'),
(25, 'Zarządzanie projektami', 13, 25, 'Pending', '2023-05-10'),
(26, 'Planowanie budżetu', 13, 26, 'Completed', '2023-01-12'),
(27, 'Wdrażanie polityki prywatności', 14, 27, 'In Progress', '2023-02-20'),
(28, 'Prowadzenie działań PR', 14, 28, 'Pending', '2023-04-05'),
(29, 'Testowanie funkcjonalności', 15, 29, 'Completed', '2023-02-12'),
(30, 'Przygotowanie oferty', 15, 30, 'In Progress', '2023-03-25'),
(31, 'Aktualizacja systemu', 16, 1, 'Pending', '2023-06-30'),
(32, 'Szkolenie techniczne', 16, 2, 'Completed', '2023-02-28'),
(33, 'Przygotowanie kampanii reklamowej', 17, 3, 'In Progress', '2023-04-10'),
(34, 'Zarządzanie budżetem działu', 17, 4, 'Pending', '2023-03-15'),
(35, 'Przygotowanie raportu rocznego', 18, 5, 'Completed', '2023-01-15'),
(36, 'Testowanie aplikacji mobilnej', 18, 6, 'In Progress', '2023-04-25'),
(37, 'Zarządzanie umowami z klientami', 19, 7, 'Pending', '2023-05-30'),
(38, 'Koordynacja działań marketingowych', 19, 8, 'Completed', '2023-02-10'),
(39, 'Analiza wyników sprzedaży', 20, 9, 'In Progress', '2023-03-20'),
(40, 'Rozwój strategii e-commerce', 20, 10, 'Pending', '2023-06-15'),
(41, 'Koordynacja zewnętrznych dostawców', 21, 11, 'Completed', '2023-02-02'),
(42, 'Ulepszanie procedur wewnętrznych', 21, 12, 'In Progress', '2023-04-10'),
(43, 'Zarządzanie zespołem IT', 22, 13, 'Pending', '2023-05-15'),
(44, 'Rozwój aplikacji mobilnej', 22, 14, 'Completed', '2023-01-25'),
(45, 'Zarządzanie projektami IT', 23, 15, 'In Progress', '2023-04-05'),
(46, 'Przygotowanie oferty sprzedażowej', 23, 16, 'Pending', '2023-05-20'),
(47, 'Szkolenie z obsługi klienta', 24, 17, 'Completed', '2023-02-12'),
(48, 'Koordynowanie działań zespołu', 24, 18, 'In Progress', '2023-03-18'),
(49, 'Zarządzanie zasobami ludzkimi', 25, 19, 'Pending', '2023-06-01'),
(50, 'Zarządzanie projektami HR', 25, 20, 'Completed', '2023-01-10');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `assets`
--
ALTER TABLE `assets`
  ADD PRIMARY KEY (`AssetID`);

--
-- Indeksy dla tabeli `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`ClientID`);

--
-- Indeksy dla tabeli `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`DepartmentID`);

--
-- Indeksy dla tabeli `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`EmployeeID`);

--
-- Indeksy dla tabeli `invoices`
--
ALTER TABLE `invoices`
  ADD PRIMARY KEY (`InvoiceID`);

--
-- Indeksy dla tabeli `locations`
--
ALTER TABLE `locations`
  ADD PRIMARY KEY (`LocationID`);

--
-- Indeksy dla tabeli `meetings`
--
ALTER TABLE `meetings`
  ADD PRIMARY KEY (`MeetingID`);

--
-- Indeksy dla tabeli `projects`
--
ALTER TABLE `projects`
  ADD PRIMARY KEY (`ProjectID`);

--
-- Indeksy dla tabeli `salaries`
--
ALTER TABLE `salaries`
  ADD PRIMARY KEY (`SalaryID`);

--
-- Indeksy dla tabeli `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`TaskID`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `assets`
--
ALTER TABLE `assets`
  MODIFY `AssetID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=73;

--
-- AUTO_INCREMENT dla tabeli `clients`
--
ALTER TABLE `clients`
  MODIFY `ClientID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT dla tabeli `departments`
--
ALTER TABLE `departments`
  MODIFY `DepartmentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT dla tabeli `employees`
--
ALTER TABLE `employees`
  MODIFY `EmployeeID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT dla tabeli `invoices`
--
ALTER TABLE `invoices`
  MODIFY `InvoiceID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=83;

--
-- AUTO_INCREMENT dla tabeli `locations`
--
ALTER TABLE `locations`
  MODIFY `LocationID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `meetings`
--
ALTER TABLE `meetings`
  MODIFY `MeetingID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `projects`
--
ALTER TABLE `projects`
  MODIFY `ProjectID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;

--
-- AUTO_INCREMENT dla tabeli `salaries`
--
ALTER TABLE `salaries`
  MODIFY `SalaryID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT dla tabeli `tasks`
--
ALTER TABLE `tasks`
  MODIFY `TaskID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
