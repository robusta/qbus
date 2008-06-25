DROP DATABASE IF EXISTS `homeapplication`;

CREATE DATABASE IF NOT EXISTS `homeapplication`
CHARACTER SET latin1;

USE `homeapplication`;

SET autocommit=0;

SET FOREIGN_KEY_CHECKS=0;

#   ----------- SYSTEEMTABELLEN -----------

CREATE TABLE `homeapplication`.`sysobject` (
	`sysobject_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`naam` varchar(50) NOT NULL DEFAULT '',
	`omschrijving` varchar(250),
	`last_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`crud` int(10) UNSIGNED,
	PRIMARY KEY(`sysobject_id`)
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysattribute` (
	`sysattribute_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`sysobject_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`sequencenr` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`naam` varchar(50) NOT NULL DEFAULT '',
	`omschrijving` varchar(250),
	`datatype` varchar(25) NOT NULL DEFAULT '',
	`enumtype` varchar(50),
	`display` varchar(25),
	`required` boolean NOT NULL DEFAULT 0,
	`enabled` boolean NOT NULL DEFAULT 0,
	`uniek` boolean NOT NULL DEFAULT 0,
	`defaultvalue` varchar(250),
	`minvalue` varchar(250),
	`maxvalue` varchar(250),
	`maxlengte` int(10) UNSIGNED,
	`crud` int(10) UNSIGNED,
	PRIMARY KEY(`sysattribute_id`),
	CONSTRAINT `FK_sysattribute-sysobject` FOREIGN KEY (`sysobject_id`)
		REFERENCES `sysobject`(`sysobject_id`)
		ON DELETE RESTRICT
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysenumeration` (
	`sysenumeration_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`enumtype` varchar(50) NOT NULL DEFAULT '',
	`naam` varchar(50) NOT NULL DEFAULT '',
	`omschrijving` varchar(250),
	`sequencenr` int(10) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY(`sysenumeration_id`),
	CONSTRAINT `FK_sysenumeration-sysattribute` FOREIGN KEY (`sysattribute_id`)
		REFERENCES `sysattribute`(`sysattribute_id`)
		ON DELETE RESTRICT
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysrelation` (
	`sysrelation_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`naam` varchar(50) NOT NULL DEFAULT '',
	`from_sysobject` varchar(50) NOT NULL DEFAULT '',
	`from_sysattribute` varchar(50) NOT NULL DEFAULT '',
	`to_sysobject` varchar(50) NOT NULL DEFAULT '',
	`to_sysattribute` varchar(50) NOT NULL DEFAULT '',
	`linktype` varchar(25) NOT NULL DEFAULT '',
	`linktable` varchar(50),
	PRIMARY KEY(`sysrelation_id`)
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`exception` (
  `exception_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `exceptiontype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `reference` VARCHAR(25),
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  `omschrijving` VARCHAR(2500),
  PRIMARY KEY(`exception_id`)
)
ENGINE = InnoDB;



#   ----------- DOMOTICATABELLEN -----------


CREATE TABLE `homeapplication`.`module` (
  `module_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `moduletype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `nummer` VARCHAR(10) NOT NULL DEFAULT '',
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  `kast_enumid` INTEGER UNSIGNED,
  PRIMARY KEY(`module_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`module_output` (
  `module_output_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `module_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `outputnummer` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `address` INTEGER UNSIGNED,
  `subaddress` INTEGER UNSIGNED,
  PRIMARY KEY(`module_output_id`),
  CONSTRAINT `FK_module_output-module` FOREIGN KEY `FK_module_output-module` (`module_id`)
    REFERENCES `module` (`module_id`)
    ON DELETE RESTRICT
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`ruimte` (
  `ruimte_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `sequencenr` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  PRIMARY KEY(`ruimte_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`output_vwzone` (
  `output_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `module_output_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `sequencenr` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  `opmerkingen` VARCHAR(500),
  `nacht` BOOLEAN,
  `nacht_temp` DOUBLE,
  `economy` BOOLEAN,
  `economy_temp` DOUBLE,
  `comfort` BOOLEAN,
  `comfort_temp` DOUBLE,
  PRIMARY KEY(`output_vwzone_id`),
  CONSTRAINT `FK_output_vwzone-module_output` FOREIGN KEY `FK_output_vwzone-module_output` (`module_output_id`)
    REFERENCES `module_output` (`module_output_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`lnk_output_vwzone_ruimte` (
  `output_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `ruimte_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  CONSTRAINT `FK_output_vwzone-ruimte_1` FOREIGN KEY `FK_output_vwzone-ruimte_1` (`output_vwzone_id`)
    REFERENCES `output_vwzone` (`output_vwzone_id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `FK_output_vwzone-ruimte_2` FOREIGN KEY `FK_output_vwzone-ruimte_2` (`ruimte_id`)
    REFERENCES `ruimte` (`ruimte_id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`logging_vwzone` (
  `logging_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `output_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `datum` DATE NOT NULL DEFAULT 0,
  `tijd` TIME NOT NULL DEFAULT 0,
  `profiel` VARCHAR(10),
  `temp_gewenst` DOUBLE,
  `temp_gemeten` DOUBLE,
  `status` VARCHAR(10),
  PRIMARY KEY(`logging_vwzone_id`),
  CONSTRAINT `FK_logging_vwzone-output_vwzone` FOREIGN KEY `FK_logging_vwzone-output_vwzone` (`output_vwzone_id`)
    REFERENCES `output_vwzone` (`output_vwzone_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`qbus_code` (
  `qbus_code_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `codetype` VARCHAR(25) NOT NULL DEFAULT '',
  `code` VARCHAR(10) NOT NULL DEFAULT '',
  `vertaling` VARCHAR(100) NOT NULL DEFAULT '',
  PRIMARY KEY(`qbus_code_id`)
)
ENGINE = InnoDB;


#   ----------- FINANCIELE TABELLEN -----------

CREATE TABLE `homeapplication`.`accounting_account` (
  `accounting_account_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `accounttype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `niveau` INTEGER UNSIGNED NOT NULL,
  `masteraccount_id` INTEGER UNSIGNED,
  `sequencenr` INTEGER UNSIGNED NOT NULL,  
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  `kostenschatting` DOUBLE,
  `aftrekbaar` BOOLEAN,
  `opmerkingen` VARCHAR(2500),
  PRIMARY KEY(`accounting_account_id`),
  CONSTRAINT `FK_accounting_account-account_account` FOREIGN KEY `FK_accounting_account-account_account` (`masteraccount_id`)
    REFERENCES `accounting_account` (`accounting_account_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`bankrekening` (
  `bankrekening_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `rekeningtype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  `bank_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `rekeningnummer` VARCHAR(25) NOT NULL DEFAULT '',
  `startsaldo` DOUBLE,
  `huidigsaldo` DOUBLE,
  `opmerkingen` VARCHAR(2500),
  PRIMARY KEY(`bankrekening_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`bankkaart` (
  `bankkaart_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `kaarttype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `naam` VARCHAR(100) NOT NULL DEFAULT '',
  `bankrekening_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `saldo` DOUBLE,
  `afrekeningperiode` INTEGER UNSIGNED,
  `afrekeningdag` VARCHAR(25),
  `opmerkingen` VARCHAR(2500),
  PRIMARY KEY(`bankkaart_id`),
  CONSTRAINT `FK_bankkaart-bankrekening` FOREIGN KEY `FK_bankkaart_bankrekening` (`bankrekening_id`)
    REFERENCES `bankrekening` (`bankrekening_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`transactie` (
  `transactie_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `transactietype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `bankrekening_id` INTEGER UNSIGNED,
  `bankkaart_id` INTEGER UNSIGNED,
  `datum` DATE NOT NULL DEFAULT 0,
  `omschrijving` VARCHAR(100) NOT NULL DEFAULT '',
  `bedrag` DOUBLE NOT NULL DEFAULT 0,
  `opmerkingen` VARCHAR(2500),
  `tegenpartij` VARCHAR(250),
  `tegenpartij_id` INTEGER UNSIGNED,
  PRIMARY KEY(`transactie_id`),
  CONSTRAINT `FK_transactie-bankrekening` FOREIGN KEY `FK_transactie_bankrekening` (`bankrekening_id`)
    REFERENCES `bankrekening` (`bankrekening_id`),
  CONSTRAINT `FK_transactie-bankkaart` FOREIGN KEY `FK_transactie_bankkaart` (`bankkaart_id`)
    REFERENCES `bankkaart` (`bankkaart_id`),
  CONSTRAINT `FK_transactie-tegenpartij` FOREIGN KEY `FK_transactie_tegenpartij` (`tegenpartij_id`)
    REFERENCES `contact_bankrekening` (`contact_bankrekening_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`accounting_transactie` (
  `accounting_transactie_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `transactie_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `accounting_account_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `datum` DATE NOT NULL DEFAULT 0,
  `bedrag` DOUBLE NOT NULL DEFAULT 0,
  `opmerkingen` VARCHAR(2500),
  PRIMARY KEY(`accounting_transactie_id`),
  CONSTRAINT `FK_accounting_transactie-transactie` FOREIGN KEY `FK_accounting_transactie_transactie` (`transactie_id`)
    REFERENCES `transactie` (`transactie_id`),
  CONSTRAINT `FK_accounting_transactie-accounting_account` FOREIGN KEY `FK_accounting_transactie_accounting_account` (`accounting_account_id`)
    REFERENCES `accounting_account` (`accounting_account_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`transactie_planned` (
  `transactie_planned_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `transactietype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `bankrekening_id` INTEGER UNSIGNED,
  `bankkaart_id` INTEGER UNSIGNED,
  `datum` DATE NOT NULL DEFAULT 0,
  `omschrijving` VARCHAR(100) NOT NULL DEFAULT '',
  `bedrag` DOUBLE NOT NULL DEFAULT 0,
  `opmerkingen` VARCHAR(2500),
  `tegenpartij` VARCHAR(250),
  `tegenpartij_id` INTEGER UNSIGNED,
  `recurring_id` INTEGER UNSIGNED,
  PRIMARY KEY(`transactie_planned_id`),
  CONSTRAINT `FK_transactie_planned-bankrekening` FOREIGN KEY `FK_transactie_planned_bankrekening` (`bankrekening_id`)
    REFERENCES `bankrekening` (`bankrekening_id`),
  CONSTRAINT `FK_transactie_planned-bankkaart` FOREIGN KEY `FK_transactie_planned_bankkaart` (`bankkaart_id`)
    REFERENCES `bankkaart` (`bankkaart_id`),
  CONSTRAINT `FK_transactie_planned-tegenpartij` FOREIGN KEY `FK_transactie_planned_tegenpartij` (`tegenpartij_id`)
    REFERENCES `contact_bankrekening` (`contact_bankrekening_id`),
  CONSTRAINT `FK_transactie_planned-recurringj` FOREIGN KEY `FK_transactie_planned_recurring` (`recurring_id`)
    REFERENCES `recurring` (`recurring_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`accounting_transactie_planned` (
  `accounting_transactie_planned_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `transactie_planned_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `accounting_account_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `datum` DATE NOT NULL DEFAULT 0,
  `bedrag` DOUBLE NOT NULL DEFAULT 0,
  `opmerkingen` VARCHAR(2500),
  PRIMARY KEY(`accounting_transactie_planned_id`),
  CONSTRAINT `FK_accounting_transactie_planned-transactie_planned` FOREIGN KEY `FK_accounting_transactie_planned_transactie_planned` (`transactie_planned_id`)
    REFERENCES `transactie_planned` (`transactie_planned_id`),
  CONSTRAINT `FK_accounting_transactie_planned-accounting_account` FOREIGN KEY `FK_accounting_transactie_planned_accounting_account` (`accounting_account_id`)
    REFERENCES `accounting_account` (`accounting_account_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`recurring` (
  `recurring_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `transactietype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `bankrekening_id` INTEGER UNSIGNED,
  `bankkaart_id` INTEGER UNSIGNED,
  `startdatum` DATE NOT NULL DEFAULT 0,
  `einddatum` DATE,
  `periode` INTEGER UNSIGNED,
  `omschrijvingstemplate` VARCHAR(100) NOT NULL DEFAULT '',
  `bedragvast` DOUBLE,
  `bedragvariabel` DOUBLE,
  `tegenpartij` VARCHAR(250),
  `tegenpartij_id` INTEGER UNSIGNED,
  `opmerkingen` VARCHAR(2500),
  PRIMARY KEY(`recurring_id`),
  CONSTRAINT `FK_recurring-bankrekening` FOREIGN KEY `FK_recurring_bankrekening` (`bankrekening_id`)
    REFERENCES `bankrekening` (`bankrekening_id`),
  CONSTRAINT `FK_recurring-bankkaart` FOREIGN KEY `FK_recurring_bankkaart` (`bankkaart_id`)
    REFERENCES `bankkaart` (`bankkaart_id`),
  CONSTRAINT `FK_recurring-tegenpartij` FOREIGN KEY `FK_recurring_tegenpartij` (`tegenpartij_id`)
    REFERENCES `contact_bankrekening` (`contact_bankrekening_id`)
)
ENGINE = InnoDB;




SET FOREIGN_KEY_CHECKS=1;

COMMIT;